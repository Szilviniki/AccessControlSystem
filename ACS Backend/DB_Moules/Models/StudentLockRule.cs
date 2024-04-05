namespace DB_Module.Models;

[Table("StudentsLockRules")]
[PrimaryKey("StudentId", "LockRuleId")]
public class StudentLockRule
{
    
    public Student Student { get; set; }
    [Column("studentId")]
    [ForeignKey("Student")]
    public Guid StudentId { get; set; }
    
    public LockRule LockRule { get; set; }
    [ForeignKey("LockRule")]
    [Column("LockRuleId")]
    public int LockRuleId { get; set; }
}