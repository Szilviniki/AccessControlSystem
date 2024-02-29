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

        public string HashPassword(string password)
        {
            var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return hash;
        }

        public bool ValidatePassword(string password, string email)
        {
            if (_sql.Personnel.Any(a => a.Email == email))
            {
                string storedPassword = _sql.Personnel.First(x => x.Email == email).Password;
                if (BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
    }

}