namespace Model;

public class Admin : User
{
    private static Admin adminInstance;
    private Admin(string name, int id,int password) : base(name, id,password)
    {
    }

    public static Admin AdminInstance
    {
        get
        {
            if (adminInstance == null)
            {
                adminInstance = new Admin("Abolfazl",0,123);
            }

            return adminInstance;
        }
    }
}