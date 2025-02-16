namespace Model
{
    public class Item
    {
        private string name;
        private int price;
        private int id;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Item(string name, int price)
        {
            this.name = name;
            this.price = price;
        }
    }
}