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
        var studentsRestricted = new HashSet<string>();
        
        var AllRules = _sql.ActiveRules.ToList();

        foreach (var entry in AllRules)
        {
            studentsRestricted.Add(entry.StudentName);
        }
        
        List<string> notices = new List<string>();
        
        var relevantRules = _sql.ActiveParoleRules.ToList().Where(x=>x.DayOfWeek == (int)DateTime.Now.DayOfWeek&&x.EndTime>DateTime.Now.TimeOfDay).ToList();
        if (relevantRules.Count == 0)
        {
            notices.Add("Nincs aktív szabály a nap hátralévő részére!");
        }
        foreach (var rule in relevantRules)
        {
            if (DateTime.Now.TimeOfDay >= rule.StartTime && DateTime.Now.TimeOfDay <= rule.EndTime)
            {
                notices.Add($"{rule.StudentName} jelenleg nincs bent {rule.ParoleName} miatt! Eddig lehet kint: {rule.EndTime.Hours}:{rule.EndTime.Minutes}");
            }
            else
            {
                notices.Add($"{rule.StartTime.Hours}:{rule.StartTime.Minutes} - {rule.EndTime.Hours}:{rule.EndTime.Minutes} {rule.StudentName} - {rule.ParoleName}");
            }
        }
        
        
        var model = new HomepageModel
        {
            PresentStudents = _sql.Students.Count(x=>x.IsPresent==true),
            AbsentStudents = _sql.Students.Count(x=>x.IsPresent==false),
            LastLogs = _sql.GateLogs.OrderByDescending(x=>x.Stamp).Take(15).ToList(),
            NaughtyStudents = studentsRestricted.Count,
            Notices = notices
        };
        return model;
    }
}