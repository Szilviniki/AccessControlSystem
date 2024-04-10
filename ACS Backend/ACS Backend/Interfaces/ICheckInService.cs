namespace ACS_Backend.Interfaces;

public interface ICheckInService
{
    public Task CheckPersonnel(int cardId);
    public Task CheckStudent(int cardId);
}