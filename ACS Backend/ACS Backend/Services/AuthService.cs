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

    public bool IsAuthenticated => throw new NotImplementedException();

    public Personnel CurrentUser => throw new NotImplementedException();

    public async Task<LoginResponseModel> Login(LoginModel login)
    {
        if (this._sql.Personnel.Any(a => a.Email == login.Email))
        {
            var user = this._sql.Personnel.Single(u => u.Email == login.Email);
            if (_encryptionService.ValidatePassword(user.Password, login.Password))
            {
                var role = this._sql.PersonRoles.FirstOrDefault(x => x.Id == user.RoleId)?.Name;
                return new LoginResponseModel()
                {
                    Email = login.Email,
                    Name = user.Name,
                    Role = role,
                    Token = _tokenService.CreateToken(user)
                };
            }
        }
        throw new FailedLoginException();
    }
}