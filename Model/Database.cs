namespace Model;

public class Database
{
    private static Database databaseSample;
    private List<User> users;
    private List<Item> items;

    private Database()
    {
        users = new List<User>();
        items = new List<Item>();
    }

    public static Database DatabaseSample
    {
        get
        {
            if (databaseSample == null)
            {
                databaseSample = new Database();
            }

            return databaseSample;
        }
    }

    public List<User> Users
    {
        get
        {
            return users;
        }
        set
        {
            users = value;
        }
    }

    public List<Item> Items
    {
        get
        {
            return items;
        }
        set
        {
            items = value;
        }
    }
}