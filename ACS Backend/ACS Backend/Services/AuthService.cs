using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace ACS_Backend.Services;

public class AuthService : IAuthService
{
    private SQL _sql;
    private IEncryptionService _encryptionService;

    public AuthService(SQL sql, IEncryptionService encryptionService)
    {
        _sql = sql;
        _encryptionService = encryptionService;
    }

    public LoginResponseModel Login(LoginModel login)
    {
        bool isEmail = _sql.Personnel.Any(a => a.Email == login.Email);
        if (isEmail)
        {
            var user = this._sql.Personnel.Single(u => u.Email == login.Email);
            bool isPassword = _encryptionService.ValidatePassword(login.Password, login.Email);
            if (isPassword)
            {
                return new LoginResponseModel
                {
                    Email = login.Email,
                    Name = user.Name,
                    Role = "something"
                };
            }
        }

        throw new FailedLoginException();

    }
}