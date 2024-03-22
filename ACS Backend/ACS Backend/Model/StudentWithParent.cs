namespace ACS_Backend.Model;

public class StudentWithParent
{
    public Guid StudentId { get; set; }
    public String StudentName { get; set; }
    public String StudentEmail { get; set; }
    public String StudentPhone { get; set; }
    public int CardId { get; set; }
    public bool IsPresent { get; set; }
    public DateTime StudentBirthDate { get; set; }

    public Guid ParentId { get; set; }
    public String ParentName { get; set; }
    public String ParentEmail { get; set; }
    public String ParentPhone { get; set; }

}