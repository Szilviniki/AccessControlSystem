namespace DB_Module.Models;

[Table("StudentsParoleRules")]
[PrimaryKey("StudentId", "ParoleRuleId")]
public class StudentParoleRule
{
    [Column("studentId")]
    [ForeignKey("FK_StudentsParoleRules_Students")]
    public Guid StudentId { get; set; }

    [Column("paroleId")]
    [ForeignKey("FK_StudentsParoleRules_StudentsParoleRules")]
    public int ParoleRuleId { get; set; }
}