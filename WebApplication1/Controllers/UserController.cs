using Microsoft.AspNetCore.Mvc;
using Service;
using Model;
using Model.DTOs;

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
    public ActionResult SignUp(SignupRequest request)
    {
        var isSignedUp = _userService.SignUp(request.name, request.password);

        if (isSignedUp)
        {
            return StatusCode(201);
        }

        return BadRequest();
    }

    [HttpPost("Login")]
    public IActionResult Login(LoginRequest request)
    {
        var isLoggedin = _userService.Login(request.name, request.password);

        return isLoggedin;
    }

    [HttpPut("ChangeInformation")]
    public ActionResult ChangeInformation(ChangeInformationRequest request)
    {
        var isChanged = _userService.ChangeInformation(request.name, request.password);
        
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

    [HttpPost("AddCar")]
    public ActionResult AddCar(Car car)
    {
        var isAdded = _itemService.AddCar(car);

        if (isAdded)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("AddBicycle")]
    public ActionResult AddBicycle(Bicycle bicycle)
    {
        var isAdded = _itemService.AddBicycle(bicycle);

        if (isAdded)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("BuyItem")]
    public ActionResult BuyItem(BuyRequest request)
    {
        var isBought = _itemService.BuyItem(request.name, request.id);

        if (isBought)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet("UsersInfo")]
    public ActionResult UsersInfo()
    {
        try
        {
            UsersInfoResponse response = _userService.UsersInfo();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpGet("ItemsInfo")]
    public ActionResult ItemsInfo()
    {
        ItemsInfoRespose response = _itemService.ItemsInfo();
        return Ok(response);
    }

    [HttpDelete("RemoveItem")]
    public ActionResult RemoveItem(RemoveItemRequest request)
    {
        var isRemoved = _itemService.RemoveItem(request.name,request.id);
        if (isRemoved)
        {
            return NoContent();
        }

        return BadRequest();
    }
}