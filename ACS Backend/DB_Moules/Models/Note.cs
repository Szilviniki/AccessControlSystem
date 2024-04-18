namespace DB_Module.Models;

[Table("Notes")]
public class Note
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Range(1,5)]
    public int DayOfWeek { get; set; }

    [ForeignKey("Id")]
    [Required] public Guid StudentId { get; set; } = Guid.Empty;
    
    public Student Student { get; set; } = null!;
}