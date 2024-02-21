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

    public HomepageModel GetHomepageData()
    {
        var model = new HomepageModel
        {
            AbsentStudents = _sql.Students.Where(x => !x.IsPresent).Select(x => x.Name).ToList(),
            LastLogs = _sql.GateLogs.OrderByDescending(x => x.Stamp).Take(10).ToList(),
            NaughtyStudents = _sql.Students.Where(w => _sql.StudentRestrictions.Any(x => x.StudentId == w.Id))
                .Select(x => x.Name).ToList(),
            NiceStudents = _sql.Students.Where(w => _sql.StudentPrivileges.Any(x => x.StudentId == w.Id))
                .Select(x => x.Name).ToList()
        };
        return model;
    }
}