using System.Data;
using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Utilities;

namespace ACS_Backend.Services;

public class PersonnelService : IPersonnelService
{
    private SQL _sql;

    private IEncryptionService _encryptionService;
    private IObjectValidatorService _objectValidatorService;

    public PersonnelService(SQL sql, IEncryptionService encryptionService,
        IObjectValidatorService objectValidatorService)
    {
        _sql = sql;
        _encryptionService = encryptionService;
        _objectValidatorService = objectValidatorService;
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

    public async Task UpdateFaculty(Personnel faculty, Guid id)
    {
        //TODO: Update Method
        if (!_sql.Personnel.Any(x => x.Id == id)) throw new ItemNotFoundException();
        if(faculty.CanLogin && string.IsNullOrWhiteSpace(faculty.Password) ==false)
        {
            if (string.IsNullOrWhiteSpace(faculty.Password)) throw new ArgumentException("Password cannot be empty");
            faculty.Password = _encryptionService.Encrypt(faculty.Password);
        }

        var valRes = _objectValidatorService.Validate(faculty);
        if (valRes.QueryIsSuccess == false) throw new ArgumentException(string.Join(", ", valRes.Data));
        // var checkRes = _checker.IsUniqueFaculty(faculty);
        // if (!checkRes.QueryIsSuccess)
        //     throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
        _sql.Personnel.Update(faculty);
        await _sql.SaveChangesAsync();
    }

    public async Task AddFaculty(Personnel faculty)
    {
        faculty.CardId = new Random().Next(100000, 999999);
        faculty.Id = Guid.NewGuid();
        if (faculty.CanLogin)
        {
            var checkRes = _checker.IsUniqueFaculty(faculty);
            if (!checkRes.QueryIsSuccess)
                throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };
            
            var uRes = _objectValidatorService.Validate(faculty);
            if (uRes.QueryIsSuccess == false) throw new ArgumentException(string.Join(", ", uRes.Data));
            
            if (string.IsNullOrWhiteSpace(faculty.Password)) throw new ArgumentException("Password cannot be empty");
            
            faculty.Password = _encryptionService.Encrypt(faculty.Password);
        }

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