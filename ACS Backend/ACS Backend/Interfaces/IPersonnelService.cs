using ACS_Backend.Model;

namespace ACS_Backend.Interfaces;

public interface IPersonnelService
{
    public Personnel GetFaculty(Guid id);
    public Array GetAllFaculties();
    public Task UpdateFaculty(UpdatePersonnelModel faculty, Guid id);
    public Task AddFaculty(Personnel faculty);
    public Task RemoveFaculty(Guid id);
}