﻿namespace ACS_Backend.Interfaces;

public interface IPersonnelService
{
    public Personnel GetFaculty(Guid id);
    public Array GetAllFaculties();
    public Task UpdateFaculty(Personnel faculty);
    public Task AddFaculty(Personnel faculty);
    public Task RemoveFaculty(Guid id);
}