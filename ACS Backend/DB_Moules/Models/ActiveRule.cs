namespace DB_Module.Models;

[Keyless]
public class ActiveRule
{
    [MaxLength(50)] public string StudentName { get; set; } = "";
    [MaxLength(100)] public string RuleName { get; set; } = "";
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int DayOfWeek { get; set; }
}