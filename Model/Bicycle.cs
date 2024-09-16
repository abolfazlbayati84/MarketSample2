namespace Model;

public class Bicycle : Item
{
    private int gearNumber;
    public int GearNumber
    {
        get { return gearNumber; }
        set { gearNumber = value; }
    }

    public Bicycle(string name, int price, int gearNumber,int id):base(name,price,id)
    {
        this.gearNumber = gearNumber;
    }
}