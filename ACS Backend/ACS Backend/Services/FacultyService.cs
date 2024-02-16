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

    public Faculty GetFaculty(Guid id)
    {
        if (!_sql.Faculties.Any(x => x.Id == id)) throw new ItemNotFoundException();
        var person = _sql.Faculties.Single(x => x.Id == id);
        return person;
    }

    public Array GetAllFaculties()
    {
        return _sql.Faculties.ToArray();
    }

    public async Task UpdateFaculty(Faculty faculty)
    {
        if (!_sql.Faculties.Any(x => x.Id == faculty.Id)) throw new ItemNotFoundException();
        var checkRes = _checker.IsUniqueFaculty(faculty);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
        _sql.Faculties.Update(faculty);
        await _sql.SaveChangesAsync();
    }

    public async Task AddFaculty(Faculty faculty)
    {
        if (_sql.Faculties.Any(x => x.CardId == faculty.CardId))
        {
            throw new ItemAlreadyExistsException();
        }
        var checkRes = _checker.IsUniqueFaculty(faculty);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
        var check =
            faculty.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(faculty.Password);
        _sql.Faculties.Add(faculty);
        await _sql.SaveChangesAsync();
    }

    public async Task RemoveFaculty(Guid id)
    {
        if (!_sql.Faculties.Any(x => x.Id == id)) throw new ItemNotFoundException();
        var user = _sql.Faculties.FirstOrDefault(x => x.Id == id);
        if (user != null) _sql.Faculties.Remove(user);
        await _sql.SaveChangesAsync();
    }
}