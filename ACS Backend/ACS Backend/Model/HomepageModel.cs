namespace ACS_Backend.Model;

public class HomepageModel
{
    public List<string> AbsentStudents { get; set; } = new List<string>();
    public List<GateLog> LastLogs { get; set; } = new List<GateLog>();
    public List<string> NaughtyStudents { get; set; } = new List<string>();
    public List<string> NiceStudents { get; set; } = new List<string>();
}