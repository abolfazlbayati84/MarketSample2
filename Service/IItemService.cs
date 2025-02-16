
using Model;
using Model.DTOs;

namespace Service;

public interface IItemService
{
    public Boolean AddCar(Car car);
    public Boolean AddBicycle(Bicycle bicycle);
    public Boolean RemoveItem(string name,string id);
    public Boolean BuyItem(string name,string id);
    public ItemsInfoRespose ItemsInfo();
}