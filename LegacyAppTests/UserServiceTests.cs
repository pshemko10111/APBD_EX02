using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTests
{
    //Arrange
    private UserService service = new UserService();

    [Fact]
    public void AddUser_Should_Return_False_When_Missing_FirstName()
    {
        //Act
        var result = service.AddUser(null, null, "kowalski@wp.pl", new DateTime(1980, 1, 1), 1);

        //Assert
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Missing_At_Sign_And_Dot_In_Email()
    {

        //Act
        var result = service.AddUser("John", "Doe", "kowalskiwppl", new DateTime(1980, 1, 1), 1);

        //Assert
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Younger_Then_21_Years_Old()
    {

        //Act
        var result = service.AddUser("John", "Doe", "kowalski@wp.pl", new DateTime(2010, 1, 1), 1);

        //Assert
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Very_Important_Client()
    {

        //Act
        var result = service.AddUser("John", "Malewski", "kowalski@wp.pl", new DateTime(1980, 1, 1), 2);

        //Assert
        Assert.Equal(true, result);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Important_Client()
    {

        //Act
        var result = service.AddUser("John", "Smith", "smith@gmail.pl", new DateTime(1980, 1, 1), 3);

        //Assert
        Assert.Equal(true, result);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Normal_Client()
    {

        //Act
        var result = service.AddUser("John", "Kwiatkowski", "kwiatkowski@wp.pl", new DateTime(1980, 1, 1), 5);

        //Assert
        Assert.Equal(true, result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Normal_Client_With_No_Credit_Limit()
    {

        //Act
        var result = service.AddUser("John", "Kowalski", "kowalski@wp.pl", new DateTime(1980, 1, 1), 1);

        //Assert
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void AddUser_Should_Throw_Exception_When_User_Does_Not_Exist()
    {

        //Act and Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = service.AddUser("John", "Unknown", "kowalski@wp.pl", new DateTime(1980, 1, 1), 100);
        });
    }
    
    [Fact]
    public void AddUser_Should_Throw_Exception_When_User_No_Credit_Limit_Exists_For_User()
    {
        
        //Act and Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = service.AddUser("John", "Andrzejewicz", "andrzejewicz@wp.pl", new DateTime(1980, 1, 1), 6);
        });
    }
}