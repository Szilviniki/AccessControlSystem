namespace ACS_Backend.Interfaces
{
    public interface IEncryptionService
    {
        public Task<bool> ValidatePassword(string password, string email);
        public Task<string> HashPassword(string password);
    }
}
