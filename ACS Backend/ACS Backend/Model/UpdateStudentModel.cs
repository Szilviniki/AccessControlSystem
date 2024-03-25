namespace ACS_Backend.Model;

public class UpdateStudentModel
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public Guid ParentId { get; set; }
    public DateTime BirthDate { get; set; }
}