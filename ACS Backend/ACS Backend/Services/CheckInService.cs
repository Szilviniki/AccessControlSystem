﻿using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;

namespace ACS_Backend.Services;

public class CheckInService : ICheckInService
{
    private SQL _sql;


    public CheckInService(SQL sql)
    {
        _sql = sql;
    }

    public async Task CheckPersonnel(int cardId)
    {
        if (!_sql.Personnel.Any(x => x.CardId == cardId))
        {
            throw new ItemNotFoundException();
        }

        var faculty = _sql.Personnel.Single(x => x.CardId == cardId);
        var log = new GateLog
        {
            Stamp = DateTime.Now,
            PersonId = faculty.CardId,
            IsGuest = false
        };
        faculty.IsPresent = !faculty.IsPresent;
        _sql.Personnel.Update(faculty);
        _sql.GateLogs.Add(log);
        await _sql.SaveChangesAsync();
    }

    public async Task CheckStudent(int cardId)
    {
        if (!_sql.Students.Any(x => x.CardId == cardId))
        {
            throw new ItemNotFoundException();
        }

        var student = _sql.Students.Single(x => x.CardId == cardId);
        var log = new GateLog
        {
            Stamp = DateTime.Now,
            PersonId = student.CardId,
            IsGuest = false
        };
        student.IsPresent = !student.IsPresent;
        _sql.Students.Update(student);
        _sql.GateLogs.Add(log);
        await _sql.SaveChangesAsync();
    }
}