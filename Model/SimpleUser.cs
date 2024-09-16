namespace Model;

public class SimpleUser : User
{
    private List<Item> items;
    
    public SimpleUser(string name, int id,int password) : base(name, id,password)
    {
        items = new List<Item>();
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