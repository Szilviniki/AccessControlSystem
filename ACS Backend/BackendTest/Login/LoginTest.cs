// Purpose: Test for the Login function in the AuthService class.

namespace BackendTest.Login;

[TestClass]
public class LoginTest
{
    private static SQL _sql = new SQL();
    private AuthService _authService = new AuthService(_sql, new TokenService(), new EncryptionService(_sql));

    [TestMethod]
    public void NoDataLogin()
    {
        var loginModel = new LoginModel
        {
            Email = "",
            Password = ""
        };
        Assert.ThrowsException<FailedLoginException>(() => _authService.Login(loginModel));
    }

    [TestMethod]
    public void BadLogin()
    {
        var loginModel = new LoginModel
        {
            Email = "jelszo@teszt.com",
            Password = "rosszjelszo"
        };
        Assert.ThrowsException<FailedLoginException>(() => _authService.Login(loginModel));
    }

    [TestMethod]
    public void GoodLogin()
    {
        var loginModel = new LoginModel
        {
            Email = "jelszo@teszt.com",
            Password = "jelszo"
        };
        Assert.AreEqual(_authService.Login(loginModel).Name, "Jelszó János");
    }
}