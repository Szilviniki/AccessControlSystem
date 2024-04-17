using ACS_Backend.Interfaces;
using ACS_Backend.Model;

namespace ACS_Backend.Services;

public class HomepageService : IHomepageService
{
    private SQL _sql;

    public HomepageService(SQL sql)
    {
        _sql = sql;
    }

    //TODO: Update HomepageService
    
    public HomepageModel GetHomepageData()
    {
        var studentsRestricted = new HashSet<string>();
        List<string> notices = new List<string>();
        
        
        
        
        var model = new HomepageModel
        {
            PresentStudents = _sql.Students.Count(x=>x.IsPresent==true),
            AbsentStudents = _sql.Students.Count(x=>x.IsPresent==false),
            LastLogs = _sql.GateLogs.OrderByDescending(x=>x.Stamp).Take(15).ToList(),
            NaughtyStudents = 80085,
            Notices = new List<string>()
        };
        return model;
    }
}