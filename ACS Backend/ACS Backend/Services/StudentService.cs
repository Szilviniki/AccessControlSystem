using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Utilities;

namespace ACS_Backend.Services;

public class StudentService : IStudentService
{
    private SQL _sql;
    private UniquenessChecker _checker = new UniquenessChecker(new SQL());
    private MatchingService _matchingService = new MatchingService();

    public StudentService(SQL sql)
    {
        _sql = sql;
    }

    public Student GetStudent(Guid id)
    {
        if (!_sql.Students.Any(x => x.Id == id)) throw new ItemNotFoundException();
        var student = _sql.Students.Single(x => x.Id == id);
        return student;
    }

    public Array GetAllStudents()
    {
        return _sql.Students.ToArray();
    }

    public async Task UpdateStudent(Student student)
    {
        if (!_sql.Students.Any(x => x.Id == student.Id))
        {
            throw new ItemNotFoundException();
        }

        var checkRes = _checker.IsUniqueStudent(student);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
        _sql.Students.Update(student);
        await _sql.SaveChangesAsync();
    }

    public async Task RemoveStudent(Guid id)
    {
        if (!_sql.Students.Any(x => x.Id == id)) throw new ItemNotFoundException();
        _sql.Students.Remove(_sql.Students.Single(x => x.Id == id));
        await _sql.SaveChangesAsync();
    }

    public async Task AddStudent(Student student)
    {
        if (string.IsNullOrEmpty(student.Name) || string.IsNullOrEmpty(student.Email) || string.IsNullOrEmpty(student.Phone) || student.CardId == 0)
        {
            throw new NotAddedException();
        }
        if(_matchingService.MatchEmail(student.Email) == false|| _matchingService.MatchPhone(student.Phone) == false)
        {
            throw new BadFormatException();
        }

        if (!_sql.Parents.Any(x => x.Id == student.ParentId)) throw new ReferredEntityNotFoundException();
        var checkRes = _checker.IsUniqueStudent(student);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
        student.Id = Guid.NewGuid();
        student.BirthDate = student.BirthDate.Date;
        _sql.Students.Add(student);
        await _sql.SaveChangesAsync();
    }

}