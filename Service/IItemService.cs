namespace Service;

public interface IItemService
{
    public Boolean AddItem(string itemType,string name, string price,string specificNumber);
    public Boolean RemoveItem(string name,string id);
    public Boolean BuyItem(string name,string id);
}