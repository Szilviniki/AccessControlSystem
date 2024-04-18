namespace ACS_Backend.Model;

public class NewNoteModel
{
    public string Name { get; set; }
    public int DayOfWeek { get; set; }
    public Guid StudentId { get; set; }
}