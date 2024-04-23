namespace ACS_Backend.Model;

public class UpdatePersonnelModel
{
    public String? Name { get; set; }
    [EmailAddress]
    public String? Email { get; set; }
    [Phone]
    public String? Phone { get; set; }
    public int? Role { get; set; }
    public String? Password { get; set; }
}