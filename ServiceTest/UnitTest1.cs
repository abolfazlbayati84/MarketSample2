using Model;
using Service;

namespace ServiceTest;

public class ServiceTest
{
    [Fact]
    public void LoginTest1()
    {
        //Arrange
        string expected = "You are successfully logged in.";
        
        //Act
        string actual = UserService.UserServiceSample.Login("Abolfazl", "123");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void LoginTest2()
    {
        //Arrange
        string expected = "Couldn't find you.";
        
        //Act
        string actual = UserService.UserServiceSample.Login("Abs", "1234");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void SignupTest()
    {
        //Arrange
        string expected = "You're signed up successfully.";
        
        //Act
        string actual = UserService.UserServiceSample.SignUp("Ab", "1234");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void ChangeInformationTest()
    {
        //Arrange
        string result = UserService.UserServiceSample.SignUp("Ab", "1234");
        string result2 = UserService.UserServiceSample.Login("Ab", "1234");
        string expected = "Your information changed successfully.";
        
        //Act
        string actual = UserService.UserServiceSample.ChangeInformation("Abs","123");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void AddItemTest()
    {
        //Arrange
        string result = UserService.UserServiceSample.Login("Abolfazl", "123");
        string expected = "Item is added successfully.";
        
        //Act
        string actual = UserService.UserServiceSample.AddItem("C","car1","10","100");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void AddItemTest2()
    {
        //Arrange
        string result1 = UserService.UserServiceSample.SignUp("Ab", "123");
        string result = UserService.UserServiceSample.Login("Ab", "123");
        string expected = "Only admin can add item.";
        
        //Act
        string actual = UserService.UserServiceSample.AddItem("C","car1","10","100");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void RemoveItemTest1()
    {
        //Arrange
        string result1 = UserService.UserServiceSample.SignUp("Ab", "123");
        string result = UserService.UserServiceSample.Login("Abolfazl", "123");
        string result2 = UserService.UserServiceSample.AddItem("C","car1","10","100");
        string result3 = UserService.UserServiceSample.Logout();
        string result4 = UserService.UserServiceSample.Login("Ab", "123");
        string expected = "Only admin can remove item.";
        
        //Act
        string actual = UserService.UserServiceSample.RemoveItem("car1", "0");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void RemoveItemTest2()
    {
        //Arrange
        string result1 = UserService.UserServiceSample.SignUp("Ab", "123");
        string result = UserService.UserServiceSample.Login("Abolfazl", "123");
        string result2 = UserService.UserServiceSample.AddItem("C","car1","10","100");
        string expected = "Couldn't find the item.";
        
        //Act
        string actual = UserService.UserServiceSample.RemoveItem("car2", "3");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void RemoveItemTest3()
    {
        //Arrange
        string result1 = UserService.UserServiceSample.SignUp("Ab", "123");
        string result = UserService.UserServiceSample.Login("Abolfazl", "123");
        int num = Database.DatabaseSample.Items.Count;
        string result2 = UserService.UserServiceSample.AddItem("C","car1","10","100");
        string expected = "Item is removed successfully";
        
        //Act
        string actual = UserService.UserServiceSample.RemoveItem("car1", ""+num+"");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void BuyTest1()
    {
        //Arrange
        string result1 = UserService.UserServiceSample.SignUp("Ab", "123");
        string result = UserService.UserServiceSample.Login("Abolfazl", "123");
        string result2 = UserService.UserServiceSample.AddItem("C","car1","10","100");
        string result3 = UserService.UserServiceSample.Logout();
        string result4 = UserService.UserServiceSample.Login("Ab", "123");
        string expected = "Purchase was successful.";
        
        //Act
        string actual = UserService.UserServiceSample.BuyItem("car1", "0");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void BuyTest2()
    {
        //Arrange
        string result1 = UserService.UserServiceSample.SignUp("Ab", "123");
        string result = UserService.UserServiceSample.Login("Abolfazl", "123");
        string result2 = UserService.UserServiceSample.AddItem("C","car1","10","100");
        string result3 = UserService.UserServiceSample.Logout();
        string result4 = UserService.UserServiceSample.Login("Ab", "123");
        string expected = "Purchase was not successful.";
        
        //Act
        string actual = UserService.UserServiceSample.BuyItem("car1", "1");
        
        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void LogoutTest()
    {
        //Arrange
        string result1 = UserService.UserServiceSample.SignUp("Ab", "123");
        string result4 = UserService.UserServiceSample.Login("Ab", "123");
        string expected = "You are logged out successfully.";
        
        //Act
        string actual = UserService.UserServiceSample.Logout();
        
        //Assert
        Assert.Equal(expected,actual);
    }
}