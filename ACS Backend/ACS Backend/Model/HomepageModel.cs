namespace ACS_Backend.Model;

public class HomepageModel
{
    public int PresentStudents { get; set; } = 0;
    public int AbsentStudents { get; set; } = 0;
    public List<GateLog> LastLogs { get; set; } = new List<GateLog>();
    public int NaughtyStudents { get; set; } =0;
    public List<string> Notices { get; set; } = new List<string>();
}