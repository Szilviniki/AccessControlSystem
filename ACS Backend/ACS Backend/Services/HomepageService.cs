using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace ACS_Backend.Services;

public class HomepageService : IHomepageService
{
    private SQL _sql;

    public HomepageService(SQL sql)
    {
        _sql = sql;
    }
    

    public HomepageModel GetHomepageData()
    {
        List<string> notes = new List<string>();
        int today = Convert.ToInt32(DateTime.Now.DayOfWeek);

        var ActiveNotes = _sql.Notes.Include(x => x.Student)
            .Where(x => DateTime.Today.DayOfWeek.CompareTo(x.DayOfWeek) == 0).ToList();
        foreach (var note in ActiveNotes)
        {
            notes.Add(note.Student.Name + " : " + note.Name);
        }


        var model = new HomepageModel
        {
            PresentStudents = _sql.Students.Count(x => x.IsPresent == true),
            AbsentStudents = _sql.Students.Count(x => x.IsPresent == false),
            LastLogs = _sql.GateLogs.OrderByDescending(x => x.Stamp).Take(15).ToList(),
            NoteCount = _sql.Notes.Count(),
            Notes = new List<string>()
        };
        return model;
    }
}