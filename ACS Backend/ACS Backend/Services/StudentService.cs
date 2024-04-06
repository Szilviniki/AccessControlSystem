using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using ACS_Backend.Utilities;
using Microsoft.EntityFrameworkCore;

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
        var student = _sql.Students.Include(x=>x.Parent).Single(x => x.Id == id);
        return student;
    }

    public Array GetAllStudents()
    {
        return _sql.Students.Include(x=>x.Parent).ToArray();
    }

    public async Task UpdateStudent(UpdateStudentModel student, Guid id)
    {

        if (!_sql.Students.Any(x => x.Id == id))
        {
            throw new ItemNotFoundException();
        }
        if (string.IsNullOrEmpty(student.Email) || string.IsNullOrEmpty(student.Name) || string.IsNullOrEmpty(student.Phone))
        {
            throw new UnprocessableEntityException();
        }
        if (_matchingService.MatchEmail(student.Email) == false || _matchingService.MatchPhone(student.Phone) == false)
        {
            throw new BadFormatException();
        }
        if(await _sql.Parents.AnyAsync(x=>x.Id == student.ParentId) == false)
        {
            throw new ReferredEntityNotFoundException();
        }
        var studentOld = await _sql.Students.SingleAsync(x => x.Id == id);
        studentOld.Phone = student.Phone;
        studentOld.Email = student.Email;
        studentOld.Name = student.Name;
        studentOld.ParentId = student.ParentId;
        studentOld.BirthDate = student.BirthDate.Date;


        var checkRes = _checker.IsUniqueStudentOnUpdate(studentOld);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
        _sql.Students.Update(studentOld);
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