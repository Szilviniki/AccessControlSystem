namespace ACS_Backend.Interfaces;

public interface IMatchingService
{
    public bool MatchEmail(string email);
    public bool MatchPhone(string phone);
}