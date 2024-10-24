using Microsoft.AspNetCore.Mvc;
using Service;
using Model;
namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private IItemService _itemService;

    public UserController(IUserService userService,IItemService itemService)
    {
        _userService = userService;
        _itemService = itemService;
    }

    [HttpPost("SignUp")]
    public ActionResult SignUp(string name, string password)
    {
        var isSignedUp = _userService.SignUp(name, password);

        if (isSignedUp)
        {
            return StatusCode(201);
        }

        return BadRequest();
    }

    [HttpPost("Login")]
    public IActionResult Login(string name, string password)
    {
        var isLoggedin = _userService.Login(name, password);

        return isLoggedin;
    }

    [HttpPut("ChangeInformation")]
    public ActionResult ChangeInformation(string newName, string newPassword)
    {
        var isChanged = _userService.ChangeInformation(newName, newPassword);
        
        if (isChanged)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("Logout")]
    public ActionResult Logout()
    {
        var isLoggedOut = _userService.Logout();

        if (isLoggedOut)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("AddItem")]
    public ActionResult AddItem(string itemType, string name, string price, string specificNumber)
    {
        var isAdded = _itemService.AddItem(itemType, name, price, specificNumber);

        if (isAdded)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("BuyItem")]
    public ActionResult BuyItem(string name, string id)
    {
        var isBought = _itemService.BuyItem(name, id);

        if (isBought)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete("RemoveItem")]
    public ActionResult RemoveItem(string name, string id)
    {
        var isRemoved = _itemService.RemoveItem(name, id);
        if (isRemoved)
        {
            return NoContent();
        }

        return BadRequest();
    }
}