using Model;

namespace Service;

public class UserService : IItemService
{
    private static UserService userServiceSample;
    private User currentUser;

    private UserService()
    {
        
    }

    public static UserService UserServiceSample
    {
        get
        {
            if (userServiceSample == null)
            {
                userServiceSample = new UserService();
            }

            return userServiceSample;
        }
    }

    public string SignUp(string name,string passwordStr)
    {
        if (!Database.DatabaseSample.Users.Contains(Admin.AdminInstance))
        {
            Database.DatabaseSample.Users.Add(Admin.AdminInstance);
        }
        int password = int.Parse(passwordStr);
        int id = Database.DatabaseSample.Users.Count;
        SimpleUser newUser = new SimpleUser(name, id,password);
        Database.DatabaseSample.Users.Add(newUser);
        return "You're signed up successfully.";
    }

    public string Login(string name, string passwordString)
    {
        if (!Database.DatabaseSample.Users.Contains(Admin.AdminInstance))
        {
            Database.DatabaseSample.Users.Add(Admin.AdminInstance);
        }
        int password = int.Parse(passwordString);
        foreach (var user in Database.DatabaseSample.Users)
        {
            if (user.Name.Equals(name) && user.Password.Equals(password))
            {
                currentUser = user;
                return "You are successfully logged in.";
            }
        }

        return "Couldn't find you.";
    }

    public string ChangeInformation(string newName, string newPasswordStr)
    {
        currentUser.Name = newName;
        int newPassword = int.Parse(newPasswordStr);
        currentUser.Password = newPassword;
        return "Your information changed successfully.";
    }

    public string AddItem(string itemType, string name, string price,string specificNumber)
    {
        if (currentUser is Admin)
        {
            if (itemType.Equals("C"))
            {
                int id = Database.DatabaseSample.Items.Count;
                Car newCar = new Car(name, int.Parse(price),int.Parse(specificNumber),id);
                Database.DatabaseSample.Items.Add(newCar);
                return "Item is added successfully.";
            }else if (itemType.Equals("B"))
            {
                int id = Database.DatabaseSample.Items.Count;
                Bicycle newBike = new Bicycle(name, int.Parse(price), int.Parse(specificNumber),id);
                Database.DatabaseSample.Items.Add(newBike);
                return "Item is added successfully.";
            }
        }

        return "Only admin can add item.";
    }

    public string RemoveItem(string name ,string id)
    {
        int itemId = int.Parse(id);
        if (currentUser is Admin)
        {
            foreach (var item in Database.DatabaseSample.Items)
            {
                if (item.Name.Equals(name) && item.Id.Equals(itemId))
                {
                    Database.DatabaseSample.Items.Remove(item);
                    return "Item is removed successfully";
                }
            }
            return "Couldn't find the item.";
        }

        return "Only admin can remove item.";
    }

    public string BuyItem(string name,string id)
    {
        int itemId = int.Parse(id);
        if (currentUser is SimpleUser)
        {
            Item selectedItem = null;
            foreach (var item in Database.DatabaseSample.Items)
            {
                if (item.Name.Equals(name) && item.Id.Equals(itemId))
                {
                    selectedItem = item;
                    break;
                }
            }
            if (selectedItem != null)
            {
                SimpleUser simpleUser = (SimpleUser)currentUser;
                if (simpleUser.Money >= selectedItem.Price)
                {
                    simpleUser.Money = simpleUser.Money - selectedItem.Price;
                    simpleUser.Items.Add(selectedItem);
                    Database.DatabaseSample.Items.Remove(selectedItem);
                    return "Purchase was successful.";
                }
            } 
        }
        return "Purchase was not successful.";
    }

    public string Logout()
    {
        currentUser = null;
        return "You are logged out successfully.";
    }
}