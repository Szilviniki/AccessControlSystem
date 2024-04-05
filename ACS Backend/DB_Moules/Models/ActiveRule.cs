namespace DB_Module.Models;

[Keyless]
public class ActiveRule
{
    public string StudentName { get; set; }
    public string RuleName { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int DayOfWeek { get; set; }
}