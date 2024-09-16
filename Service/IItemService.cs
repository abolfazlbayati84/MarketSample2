namespace Service;

public interface IItemService
{
    public string AddItem(string itemType,string name, string price,string specificNumber);
    public string RemoveItem(string name,string id);
    public string BuyItem(string name,string id);
}