﻿using ACS_Backend.Exceptions;
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

        public string Encrypt(string password)
        {
            var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return hash;
        }

        public bool Valdiate(string password, string email)
        {
            if (_sql.Personnel.Any(a => a.Email == email))
            {
                var storedPassword = _sql.Personnel.First(x => x.Email == email).Password;
                if (BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword)) return true;
            }
            return false;
        }
    }

}