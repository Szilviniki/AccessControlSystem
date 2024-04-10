using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using ACS_Backend.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ACS_Backend.Services;

public class StudentService : IStudentService
{
    private SQL _sql;
    private UniquenessChecker _checker = new UniquenessChecker(new SQL());
    private ValidatorService _validatorService = new ValidatorService();

    public StudentService(SQL sql)
    {
        _sql = sql;
    }

    private bool _IsItFilledOut(Student student)
    {
        return !string.IsNullOrEmpty(student.Name) && !string.IsNullOrEmpty(student.Email) &&
               !string.IsNullOrEmpty(student.Phone);
    }

    public Student GetStudent(Guid id)
    {
        if (!_sql.Students.Any(x => x.Id == id)) throw new ItemNotFoundException();
        var student = _sql.Students.Include(x => x.Parent).Single(x => x.Id == id);
        return student;
    }

    public Array GetAllStudents()
    {
        return _sql.Students.ToArray();
    }

    public async Task UpdateStudent(UpdateStudentModel student, Guid id)
    {
        if (student.ParentId==null || student.Name.IsNullOrEmpty() || student.Email.IsNullOrEmpty() || student.Phone.IsNullOrEmpty() || student.BirthDate == null)
            throw new ArgumentException("Nem adtál meg módosítandó adatot");

        if (!_sql.Students.Any(x => x.Id == id))
        {
            throw new ItemNotFoundException();
        }

        if (student.ParentId != null && !_sql.Parents.Any(x => x.Id == student.ParentId))
            throw new ReferredEntityNotFoundException();

        var studentOld = await _sql.Students.SingleAsync(x => x.Id == id);
        if (!student.Email.IsNullOrEmpty())
        {
            if (!_validatorService.ValidateEmail(student.Email))
                throw new BadFormatException() { Message = "Email cím formátuma nem megfelelő" };
            studentOld.Email = student.Email;
        }

        if (!student.Phone.IsNullOrEmpty())
        {
            if (!_validatorService.ValidatePhone(student.Phone))
                throw new BadFormatException() { Message = "Telefonszám formátuma nem megfelelő" };
            studentOld.Phone = student.Phone;
        }

        if (student.Name != null)
        {
            studentOld.Name = student.Name;
        }

        if (student.ParentId != Guid.Empty && student.ParentId != null)
        {
            studentOld.ParentId = student.ParentId.GetValueOrDefault();
        }

        if (student.BirthDate != null && student.BirthDate != DateTime.MinValue)
        {
            studentOld.BirthDate = student.BirthDate.GetValueOrDefault();
        }

        studentOld.Id = id;

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
        if (!_IsItFilledOut(student))
        {
            throw new NotAddedException();
        }

        if (!_validatorService.ValidateEmail(student.Email))
        {
            throw new BadFormatException() { Message = "Email cím formátuma nem megfelelő" };
        }

        if (!_validatorService.ValidatePhone(student.Email))
        {
            throw new BadFormatException() { Message = "Telefonszám formátuma nem megfelelő" };
        }

        if (!_validatorService.ValidateBirthDate(student.BirthDate))
        {
            throw new BadFormatException() { Message = "Születési dátum formátuma nem megfelelő" };
        }

        if (!_sql.Parents.Any(x => x.Id == student.ParentId)) throw new ReferredEntityNotFoundException();
        var checkRes = _checker.IsUniqueStudent(student);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };

        student.Id = Guid.NewGuid();
        student.BirthDate = student.BirthDate.Date;
        student.CardId = new Random().Next(100000, 999999);

        _sql.Students.Add(student);
        await _sql.SaveChangesAsync();
    }

    public async Task AddStudentWithParent(Student student, Guardian parent)
    {
        await using var transaction = await _sql.Database.BeginTransactionAsync();
        try
        {
            if (string.IsNullOrEmpty(student.Name) || string.IsNullOrEmpty(student.Email) ||
                string.IsNullOrEmpty(student.Phone) || student.CardId == 0)
            {
                throw new ArgumentException("Diák mező(i) hiányzik/hiányoznak");
            }

            if (string.IsNullOrEmpty(parent.Name) || string.IsNullOrEmpty(parent.Phone) ||
                string.IsNullOrEmpty(parent.Email))
            {
                throw new ArgumentException("Törvényes Képviselő mezője/hiányoznak hiányzik/hiányoznak");
            }

            if (_validatorService.ValidateEmail(student.Email) == false ||
                _validatorService.ValidatePhone(student.Phone) == false)
            {
                throw new BadFormatException(){Message = "Email cím vagy telefonszám formátuma nem megfelelő diáknál"};
            }

            if (_validatorService.ValidatePhone(parent.Phone) == false ||
                _validatorService.ValidateEmail(parent.Email) == false)
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