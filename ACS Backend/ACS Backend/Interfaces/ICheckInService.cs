namespace ACS_Backend.Interfaces;

public interface ICheckInService
{
    public Task CheckFaculty(int cardId);
    public Task CheckStudent(int cardId);
}