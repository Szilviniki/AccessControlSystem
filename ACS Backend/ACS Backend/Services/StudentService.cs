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

    private bool _IsItFilledOut(Student student)
    {
        return !string.IsNullOrEmpty(student.Name) && !string.IsNullOrEmpty(student.Email) && !string.IsNullOrEmpty(student.Phone);
    }
    
    public Student GetStudent(Guid id)
    {
        if (!_sql.Students.Any(x => x.Id == id)) throw new ItemNotFoundException();
        var student = _sql.Students.Include(x=>x.Parent).Single(x => x.Id == id);
        return student;
    }

    public Array GetAllStudents()
    {
        return _sql.Students.ToArray();
    }

    public async Task UpdateStudent(UpdateStudentModel student, Guid id)
    {

        if (!_sql.Students.Any(x => x.Id == id))
        {
            throw new ItemNotFoundException();
        }
        var tempS = new Student(){Email = student.Email, Phone = student.Phone, Name = student.Name, ParentId = student.ParentId, BirthDate = student.BirthDate};
        if(!_IsItFilledOut(tempS))
               throw new ArgumentException("Nincs minden szükséges mező kitöltve!");
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

    public async Task AddStudentWithParent(Student student, Guardian parent)
    {
        await using var transaction = await _sql.Database.BeginTransactionAsync();
        try
        {
            if (string.IsNullOrEmpty(student.Name) || string.IsNullOrEmpty(student.Email) || string.IsNullOrEmpty(student.Phone) || student.CardId == 0)
            {
                throw new ArgumentException("Diák mező(i) hiányzik/hiányoznak");

            }
            if (string.IsNullOrEmpty(parent.Name) || string.IsNullOrEmpty(parent.Phone)|| string.IsNullOrEmpty(parent.Email))
            {
                throw new ArgumentException("Törvényes Képviselő mezője/hiányoznak hiányzik/hiányoznak");
            }
            if (_matchingService.MatchEmail(student.Email) == false || _matchingService.MatchPhone(student.Phone) == false)
            {
                throw new BadFormatException();
            }
            if (_matchingService.MatchPhone(parent.Phone) == false|| _matchingService.MatchEmail(parent.Email) == false)
            {
                throw new BadFormatException();
            }
            
            
            var checkRes = _checker.IsUniqueStudent(student);
            if (!checkRes.QueryIsSuccess)
                throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
            
            var uniqueParent = _checker.IsUniqueGuardian(parent);
            if (!uniqueParent.QueryIsSuccess)
                throw new UniqueConstraintFailedException<List<string>> { FailedOn = uniqueParent.Data };
            
            student.Id = Guid.NewGuid();
            parent.Id = Guid.NewGuid();
            student.BirthDate = student.BirthDate.Date;
            student.ParentId = parent.Id;
            _sql.Students.Add(student);
            _sql.Parents.Add(parent);
            await _sql.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch 
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}