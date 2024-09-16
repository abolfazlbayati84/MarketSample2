namespace Model;

public class User
{
    private string name;
    private int id;
    private int password;
    private int money;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public int Password
    {
        get { return password; }
        set { password = value; }
    }
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    

    public User(string name,int id,int password){
        this.name = name;
        this.id = id;
        this.password = password;
        this.money = 100;
    }

}