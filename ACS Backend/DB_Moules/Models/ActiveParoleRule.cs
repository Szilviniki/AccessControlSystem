namespace DB_Module.Models;

[Keyless]
public class ActiveParoleRule
{
 public string StudentName { get; set; }
 public string ParoleName { get; set; }
 public TimeSpan StartTime { get; set; }
 public TimeSpan EndTime { get; set; }
 public int DayOfWeek { get; set; }
}