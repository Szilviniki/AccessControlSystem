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
    private ObjectValidatorService _objectValidatorService = new ObjectValidatorService();

    public StudentService(SQL sql)
    {
        _sql = sql;
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
        if (student.ParentId == null && student.Name.IsNullOrEmpty() && student.Email.IsNullOrEmpty() &&
            student.Phone.IsNullOrEmpty() && student.BirthDate == null)
            throw new ArgumentException("Nem adtál meg módosítandó adatot");

        if (!_sql.Students.Any(x => x.Id == id))
        {
            throw new ItemNotFoundException();
        }

        var studentOld = await _sql.Students.SingleAsync(x => x.Id == id);

        if (student.ParentId != null && !_sql.Parents.Any(x => x.Id == student.ParentId))
            throw new ReferredEntityNotFoundException();


        if (!student.Email.IsNullOrEmpty())
        {
            studentOld.Email = student.Email;
        }


        if (!student.Phone.IsNullOrEmpty())
        {
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

        var checkValid = _objectValidatorService.Validate(studentOld);
        if (!checkValid.QueryIsSuccess)
            throw new ArgumentException(string.Join('|', checkValid.Data));

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
        if (!_sql.Parents.Any(x => x.Id == student.ParentId)) throw new ReferredEntityNotFoundException();

        var valRes = _objectValidatorService.Validate(student);
        if (!valRes.QueryIsSuccess)
        {
            throw new ArgumentException(string.Join('|', valRes.Data));
        }

        var checkRes = _checker.IsUniqueStudent(student);
        if (!checkRes.QueryIsSuccess)
        {
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
        }

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
            bool diditfail = false;
            string failed = "";
            var valResStudent = _objectValidatorService.Validate(student);
            var valResParent = _objectValidatorService.Validate(parent);
            
            if (!valResStudent.QueryIsSuccess)
            {
                diditfail = true;
                failed += $"Student: {string.Join(';', valResStudent.Data)}";
            }

            if (!valResParent.QueryIsSuccess)
            {
                if(diditfail)
                    failed += " | ";
                diditfail = true;
                failed += $"Parent: {string.Join(';', valResParent.Data)}";
            }

            if (diditfail)
            {
                throw new ArgumentException(failed);
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