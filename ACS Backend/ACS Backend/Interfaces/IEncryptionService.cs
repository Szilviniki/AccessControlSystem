namespace ACS_Backend.Interfaces
{
    public interface IEncryptionService
    {
        public bool ValidatePassword(string password, string email);
        public string HashPassword(string password);
    }
}
