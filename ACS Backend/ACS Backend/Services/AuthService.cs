using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;

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

    public async Task<LoginResultModel> Login(LoginModel login)
    {
        if (this._sql.Personnel.Any(a => a.Email == login.Email))
        {
            var user = this._sql.Personnel.Single(u => u.Email == login.Email);
            if (_encryptionService.ValidatePassword(user.Password, login.Password))
            {
                user.Roles = this._sql.UserRoles.Where(a => a.UserId == user.Id).Select(a => a.RoleId).ToList();
                return new LoginResponse()
                {
                    Email = login.Email,
                    Name = user.Name,
                    Roles = user.Roles,
                    Token = _tokenService.CreateToken(user)
                };
            }
            return new LoginResultModel() { Email = login.Email, Name = "Test" };
        }
        else
        {
            return new LoginResultModel() { Email = login.Email, Name = "Test" };
        }
    }
}