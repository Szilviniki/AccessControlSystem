namespace DB_Module.Models;

[Table("StudentsLockRules")]
[PrimaryKey("StudentId", "LockRuleId")]
public class StudentLockRule
{
    
    public Student Student { get; set; }
    [Column("studentId")]
    public Guid StudentId { get; set; }
    
    public LockRule LockRule { get; set; }
    [Column("LockRuleId")]
    public int LockRuleId { get; set; }
}