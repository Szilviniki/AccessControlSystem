namespace DB_Module.Models;

[Keyless]
public class ActiveParoleRule
{
    [MaxLength(50)] public string StudentName { get; set; } = "";
    [MaxLength(100)] public string ParoleName { get; set; } = "";
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int DayOfWeek { get; set; }
}