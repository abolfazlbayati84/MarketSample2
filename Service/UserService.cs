using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Model.DTOs;

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

        public Boolean AddCar(Car car)
        {
            string currentUserName = Session?.GetString("CurrentUser");
            var currentUser = Database.DatabaseSample.Users.FirstOrDefault(u => u.Name.Equals(currentUserName));

            if (currentUser is Admin)
            {
                car.Id = Database.DatabaseSample.Items.Count;
                Database.DatabaseSample.Items.Add(car);
                return true;
            }

            return false;
        }

        public Boolean AddBicycle(Bicycle bicycle)
        {
            string currentUserName = Session?.GetString("CurrentUser");
            var currentUser = Database.DatabaseSample.Users.FirstOrDefault(u => u.Name.Equals(currentUserName));

            if (currentUser is Admin)
            {
                bicycle.Id = Database.DatabaseSample.Items.Count;
                Database.DatabaseSample.Items.Add(bicycle);
                return true;
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

        public UsersInfoResponse UsersInfo()
        {
            string currentUserName = Session?.GetString("CurrentUser");
            var currentUser = Database.DatabaseSample.Users.FirstOrDefault(u => u.Name.Equals(currentUserName));

            if (currentUser is Admin)
            {
                UsersInfoResponse response = new UsersInfoResponse();
                foreach(User user in Database.DatabaseSample.Users)
                {
                    string str = "Name : " + user.Name + "      ID: " + user.Id;
                    response.users.Add(str);
                }

                return response;
            }

            throw new Exception();
        }

        public ItemsInfoRespose ItemsInfo()
        {
            ItemsInfoRespose response = new ItemsInfoRespose();
            foreach (Item item in Database.DatabaseSample.Items)
            {
                string str = "Name: " + item.Name + "      ID: " + item.Id;
                response.items.Add(str);
            }

            return response;
        }

        public Boolean Logout()
        { 
            _httpContextAccessor.HttpContext.Session.Clear();
            return true;
        }
    }
}
