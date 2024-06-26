﻿using System.Data;
using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
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

    public async Task UpdateFaculty(UpdatePersonnelModel faculty, Guid id)
    {
        if(id == Guid.Empty) throw new ArgumentException("Id cannot be empty");
        
        if (!_sql.Personnel.Any(x => x.Id == id)) throw new ItemNotFoundException();

        var old = await _sql.Personnel.FindAsync(id);

        if (!string.IsNullOrWhiteSpace(faculty.Name))
            old.Name = faculty.Name;
        if (!string.IsNullOrWhiteSpace(faculty.Email))
            old.Email = faculty.Email;
        if (!string.IsNullOrWhiteSpace(faculty.Phone))
            old.Phone = faculty.Phone;

        if (faculty.Role != null)
        {
            old.Role = (int)faculty.Role;
        }


        if (old.CanLogin && string.IsNullOrWhiteSpace(faculty.Password) == false)
        {
            var password = faculty.Password;
            old.Password = _encryptionService.Encrypt(password);
        }

        var valRes = _objectValidatorService.Validate(old);
        if (valRes.QueryIsSuccess == false) throw new ArgumentException(string.Join(", ", valRes.Data));
        
        _sql.Personnel.Update(old);
        await _sql.SaveChangesAsync();
    }

    public async Task AddFaculty(Personnel faculty)
    {
        faculty.CardId = new Random().Next(100000, 999999);
        faculty.Id = Guid.NewGuid();

        var checkRes = _checker.IsUniqueFaculty(faculty);
        if (!checkRes.QueryIsSuccess)
            throw new UniqueConstraintFailedException<List<string>> { FailedOn = checkRes.Data };

        var uRes = _objectValidatorService.Validate(faculty);
        if (uRes.QueryIsSuccess == false) throw new ArgumentException(string.Join(", ", uRes.Data));

        if (string.IsNullOrWhiteSpace(faculty.Password) && faculty.CanLogin)
            throw new ArgumentException("Password cannot be empty");

        faculty.Password = _encryptionService.Encrypt(faculty.Password);

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