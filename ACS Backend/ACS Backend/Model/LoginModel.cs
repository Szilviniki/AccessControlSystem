namespace ACS_Backend.Model;

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResultModel{
    public string Email { get; set; }
    public string Name { get; set; }
    public Role Role { get; set; }
    //TODO: add generated token
}