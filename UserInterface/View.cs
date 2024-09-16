using Service;

namespace UserInterface;

public class View
{
    private static View viewInstance;

    private View()
    {
        
    }

    public static View ViewInstance
    {
        get
        {
            if (viewInstance == null)
            {
                viewInstance = new View();
            }

            return viewInstance;
        }
    }

    public void InitialMethod()
    {
        string command = Console.ReadLine();
        string[] str = command.Split(" _");

        while (!str[0].Equals("exit"))
        {
            if (str[0].Equals("signup"))
            {
                string result = UserService.UserServiceSample.SignUp(str[1], str[2]);
                Console.WriteLine(result);
            }else if(str[0].Equals("login"))
            {
                string result = UserService.UserServiceSample.Login(str[1], str[2]);
                Console.WriteLine(result);
                if (result.Equals("You are successfully logged in."))
                {
                    SecondaryMethod();
                }
            }

            command = Console.ReadLine();
            str = command.Split(" _");
        }

    }

    public void SecondaryMethod()
    {
        string command = Console.ReadLine();
        string[] str = command.Split(" _");

        
        while(true)
        {
            if (str[0].Equals("change info"))
            {
                string result = UserService.UserServiceSample.ChangeInformation(str[1],str[2]);
                Console.WriteLine(result);
            }else if (str[0].Equals("buy"))
            {
                string result = UserService.UserServiceSample.BuyItem(str[1], str[2]);
                Console.WriteLine(result);
            }else if (str[0].Equals("remove"))
            {
                string result = UserService.UserServiceSample.RemoveItem(str[1], str[2]);
                Console.WriteLine(result);
            }else if (str[0].Equals("add"))
            {
                string result = UserService.UserServiceSample.AddItem(str[1], str[2], str[3], str[4]);
                Console.WriteLine(result);
            }else if (str[0].Equals("logout"))
            {
                string result = UserService.UserServiceSample.Logout();
                Console.WriteLine(result);
                break;
            }else if (str[0].Equals("exit"))
            {
                Environment.Exit(0);
            }

            command = Console.ReadLine();
            str = command.Split(" _");
        }
    }
}