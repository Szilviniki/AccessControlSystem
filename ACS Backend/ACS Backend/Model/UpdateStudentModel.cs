using DB_Module.Attributes;

namespace ACS_Backend.Model;

public class UpdateStudentModel
{
    public string? Name { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [PhoneNumber]
    public string? Phone { get; set; }

    public Guid? ParentId { get; set; }
    [BirthDate]
    public DateTime? BirthDate { get; set; }
    public UpdateStudentModel DeepCopy()
    {
        return new UpdateStudentModel
        {
            Name = this.Name,
            Email = this.Email,
            Phone = this.Phone,
            BirthDate = new DateTime(this.BirthDate.Value.Ticks),
            ParentId = this.ParentId
        };
    }
}