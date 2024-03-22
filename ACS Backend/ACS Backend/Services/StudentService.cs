using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using ACS_Backend.Utilities;

namespace ACS_Backend.Services;

public class StudentService : IStudentService
{
    private SQL _sql;
    private UniquenessChecker _checker = new UniquenessChecker(new SQL());

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
        if (_sql.Students.Any(x => x.CardId == student.CardId))
        {
            throw new ItemAlreadyExistsException();
        }

        var checkRes = _checker.IsUniqueStudent(student);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
        student.Id = Guid.NewGuid();
        student.BirthDate = student.BirthDate.Date;
        _sql.Students.Add(student);
        await _sql.SaveChangesAsync();
    }

    public async Task AddStudentWithGuardian(StudentWithParent studentWithParent)
    {
        var student = new Student
        {
            Name = studentWithParent.StudentName,
            Email = studentWithParent.StudentEmail,
            IsPresent = studentWithParent.IsPresent,
            Phone = studentWithParent.StudentPhone,
            BirthDate = studentWithParent.StudentBirthDate,
            CardId = studentWithParent.CardId
        };
        var guardian = new Guardian
        {
            Name = studentWithParent.ParentName,
            Email = studentWithParent.ParentEmail,
            Phone = studentWithParent.ParentPhone
        };
        var match = new MatchingService();
        if (!match.MatchPhone(student.Phone)||!match.MatchEmail(student.Email)||!match.MatchPhone(guardian.Phone)||!match.MatchEmail(guardian.Email))
            throw new BadFormatException();
        var checkRes = _checker.IsUniqueStudent(student);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };

        student.Id = Guid.NewGuid();
        guardian.Id = Guid.NewGuid();
        student.ParentId = guardian.Id;
        _sql.Students.Add(student);
        _sql.Parents.Add(guardian);
        await _sql.SaveChangesAsync();
    }
}