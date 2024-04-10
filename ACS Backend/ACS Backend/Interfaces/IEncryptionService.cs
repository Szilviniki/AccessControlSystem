namespace ACS_Backend.Interfaces
{
    public interface IEncryptionService
    {
        public bool Valdiate(string password, string email);
        public string Encrypt(string password);
    }
}
