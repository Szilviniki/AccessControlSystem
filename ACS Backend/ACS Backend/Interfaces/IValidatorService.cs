namespace ACS_Backend.Interfaces;

public interface IValidatorService
{
    public bool ValidateEmail(string email);
    public bool ValidatePhone(string phone);
    
    public bool ValidateBirthDate(DateTime birthDate);
}