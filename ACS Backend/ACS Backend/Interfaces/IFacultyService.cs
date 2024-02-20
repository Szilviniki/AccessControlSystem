namespace ACS_Backend.Interfaces;

public interface IFacultyService
{
    public Faculty GetFaculty(Guid id);
    public Array GetAllFaculties();
    public Task UpdateFaculty(Faculty faculty);
    public Task AddFaculty(Faculty faculty);
    public Task RemoveFaculty(Guid id);
}