using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Service
{
    public class UserService : IItemService, IUserService
    {
        private static UserService userServiceSample;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            if (!Database.DatabaseSample.Users.Contains(Admin.AdminInstance))
            {
                Database.DatabaseSample.Users.Add(Admin.AdminInstance);
            }
        }

        // Property to access the current session
        private ISession Session => _httpContextAccessor.HttpContext?.Session;

        public Boolean SignUp(string name, string passwordStr)
        {
            if (!Database.DatabaseSample.Users.Contains(Admin.AdminInstance))
            {
                Database.DatabaseSample.Users.Add(Admin.AdminInstance);
            }
            int password = int.Parse(passwordStr);
            int id = Database.DatabaseSample.Users.Count;
            SimpleUser newUser = new SimpleUser(name, id, password);
            Database.DatabaseSample.Users.Add(newUser);
            return true;
        }

        public IActionResult Login(string name, string passwordString)
        {
            int password = int.Parse(passwordString);
            var user = Database.DatabaseSample.Users.FirstOrDefault(u => u.Name.Equals(name) && u.Password.Equals(password));

            if (user != null)
            {
                // Use session to store the current user's information
                Session?.SetString("CurrentUser", name);
                return new OkObjectResult("Login successful.");
            }

            return new UnauthorizedResult();
        }

        public Boolean ChangeInformation(string newName, string newPasswordStr)
        {
            string currentUserName = Session?.GetString("CurrentUser");
            var currentUser = Database.DatabaseSample.Users.FirstOrDefault(u => u.Name.Equals(currentUserName));

            if (currentUser != null)
            {
                currentUser.Name = newName;
                currentUser.Password = int.Parse(newPasswordStr);
                return true;
            }

            return false;
        }

        public Boolean AddItem(string itemType, string name, string price, string specificNumber)
        {
            string currentUserName = Session?.GetString("CurrentUser");
            var currentUser = Database.DatabaseSample.Users.FirstOrDefault(u => u.Name.Equals(currentUserName));

            if (currentUser is Admin)
            {
                if (itemType.Equals("C"))
                {
                    int id = Database.DatabaseSample.Items.Count;
                    Car newCar = new Car(name, int.Parse(price), int.Parse(specificNumber), id);
                    Database.DatabaseSample.Items.Add(newCar);
                    return true;
                }
                else if (itemType.Equals("B"))
                {
                    int id = Database.DatabaseSample.Items.Count;
                    Bicycle newBike = new Bicycle(name, int.Parse(price), int.Parse(specificNumber), id);
                    Database.DatabaseSample.Items.Add(newBike);
                    return true;
                }
            }

            return false;
        }

        public Boolean RemoveItem(string name, string id)
        {
            string currentUserName = Session?.GetString("CurrentUser");
            var currentUser = Database.DatabaseSample.Users.FirstOrDefault(u => u.Name.Equals(currentUserName));

            if (currentUser is Admin)
            {
                int itemId = int.Parse(id);
                var item = Database.DatabaseSample.Items.FirstOrDefault(i => i.Name.Equals(name) && i.Id == itemId);

                if (item != null)
                {
                    Database.DatabaseSample.Items.Remove(item);
                    return true;
                }
            }

            return false;
        }

        public Boolean BuyItem(string name, string id)
        {
            string currentUserName = Session?.GetString("CurrentUser");
            var currentUser = Database.DatabaseSample.Users.FirstOrDefault(u => u.Name.Equals(currentUserName)) as SimpleUser;

            if (currentUser != null)
            {
                int itemId = int.Parse(id);
                var selectedItem = Database.DatabaseSample.Items.FirstOrDefault(i => i.Name.Equals(name) && i.Id == itemId);

                if (selectedItem != null && currentUser.Money >= selectedItem.Price)
                {
                    currentUser.Money -= selectedItem.Price;
                    currentUser.Items.Add(selectedItem);
                    Database.DatabaseSample.Items.Remove(selectedItem);
                    return true;
                }
            }

            return false;
        }

        public Boolean Logout()
        {
            // Clear session to log out
            _httpContextAccessor.HttpContext.Session.Clear();
            return true;
        }
    }
}
