using Microsoft.AspNetCore.Mvc;

namespace Service;

public interface IUserService
{
    public Boolean SignUp(string name, string passwordStr);
    public IActionResult Login(string name, string passwordString);
    public Boolean ChangeInformation(string newName, string newPasswordStr);
    public Boolean Logout();
}