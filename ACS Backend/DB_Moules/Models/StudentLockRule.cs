namespace DB_Module.Models;

[Table("StudentsLockRules")]
[PrimaryKey("StudentId", "LockRuleId")]
public class StudentLockRule
{
    [Column("studentId")] public Guid StudentId { get; set; }

    [Column("LockRuleId")] public int LockRuleId { get; set; }
}