using System.Data;
using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Utilities;

namespace ACS_Backend.Services;

public class FacultyService : IFacultyService
{
    private SQL _sql;

    public FacultyService(SQL sql)
    {
        _sql = sql;
    }

    private UniquenessChecker _checker = new UniquenessChecker(new SQL());

    public Personnel GetFaculty(Guid id)
    {
        if (!_sql.Personnel.Any(x => x.Id == id)) throw new ItemNotFoundException();
        var person = _sql.Personnel.Single(x => x.Id == id);
        return person;
    }

    public Array GetAllFaculties()
    {
        return _sql.Personnel.ToArray();
    }

    public async Task UpdateFaculty(Personnel faculty)
    {
        if (!_sql.Personnel.Any(x => x.Id == faculty.Id)) throw new ItemNotFoundException();
        var checkRes = _checker.IsUniqueFaculty(faculty);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
        _sql.Personnel.Update(faculty);
        await _sql.SaveChangesAsync();
    }

    public async Task AddFaculty(Personnel faculty)
    {
        if (_sql.Personnel.Any(x => x.CardId == faculty.CardId))
        {
            throw new ItemAlreadyExistsException();
        }
        var checkRes = _checker.IsUniqueFaculty(faculty);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
        var check =
            faculty.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(faculty.Password);
        _sql.Personnel.Add(faculty);
        await _sql.SaveChangesAsync();
    }

    public async Task RemoveFaculty(Guid id)
    {
        if (!_sql.Personnel.Any(x => x.Id == id)) throw new ItemNotFoundException();
        var user = _sql.Personnel.FirstOrDefault(x => x.Id == id);
        if (user != null) _sql.Personnel.Remove(user);
        await _sql.SaveChangesAsync();
    }
}