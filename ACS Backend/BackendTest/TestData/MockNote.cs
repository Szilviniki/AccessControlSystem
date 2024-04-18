using DB_Module.Models;

namespace DB_Module.MockData;

public class MockNote
{
    public Note Note = new Note
    {
        StudentId = Guid.Parse("4a7c7d89-f600-4f6a-9ec8-55602d7a1979"),
        Name = "Note text",
        DayOfWeek = Convert.ToInt32(DateTime.Now.DayOfWeek)
    };

    public Note DeepCopy()
    {
        return new Note
        {
            Id = Note.Id,
            StudentId = Note.StudentId,
            Name = Note.Name,
            DayOfWeek = Note.DayOfWeek
        };
    }
}