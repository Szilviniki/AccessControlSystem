namespace ACS_Backend.Interfaces;

public interface IMatchingService
{
    bool MatchEmail(string email);
    bool MatchPhone(string phone);
}