namespace Model;

public class Car : Item
{
    private int speed;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public Car(string name, int price, int speed):base(name,price)
    {
        this.speed = speed;
    }
}