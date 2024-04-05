namespace DB_Module.Models;

[Table("StudentsParoleRules")]
[PrimaryKey("StudentId", "ParoleRuleId")]
public class StudentParoleRule
{
    public Student Student { get; set; }
    [Column("studentId")]
    [ForeignKey("FK_StudentsParoleRules_Students")]
    public Guid StudentId { get; set; }
    
    public ParoleRule ParoleRule { get; set; }
    [Column("paroleId")]
    [ForeignKey("FK_StudentsParoleRules_StudentsParoleRules")]
    public int ParoleRuleId { get; set; }
}