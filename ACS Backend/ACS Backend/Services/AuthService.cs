using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace ACS_Backend.Services;

public class AuthService : IAuthService
{
    private SQL _sql;
    private IEncryptionService _encryptionService;
    private ITokenService _tokenService;

    public AuthService(SQL sql, ITokenService tokenService, IEncryptionService encryptionService)
    {
        _sql = sql;
        _tokenService = tokenService;
        _encryptionService = encryptionService;
    }

    public LoginResponseModel Login(LoginModel login)
    {
        bool isEmail = _sql.Personnel.Any(a => a.Email == login.Email);
        if (isEmail)
        {
            var user = this._sql.Personnel.Include(x => x.Role).Single(u => u.Email == login.Email);
            bool isPassword = _encryptionService.Valdiate(login.Password, login.Email);
            if (isPassword)
            {
                return new LoginResponseModel
                {
                    Email = login.Email,
                    Name = user.Name,
                    Role = user.Role.Name,
                    Token = _tokenService.CreateToken(user)
                };
            }
        }
        throw new FailedLoginException();
    }
}