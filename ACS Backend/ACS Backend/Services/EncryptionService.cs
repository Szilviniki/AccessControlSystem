using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;

namespace ACS_Backend.Services
{
    public class EncryptionService : IEncryptionService
    {
        private SQL _sql;

        public EncryptionService(SQL sql)
        {
            _sql = sql;
        }

        public async Task<string> HashPassword(string password)
        {
            var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return hash;
        }

        public async Task<bool> ValidatePassword(string password, string email)
        {
            if (_sql.Faculties.Any(a => a.Email == email)) throw new FailedLoginException();
            var user = _sql.Faculties.Single(u => u.Email == email);
            if (BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password)) return true;
            else throw new FailedLoginException();
        }
    }
}
