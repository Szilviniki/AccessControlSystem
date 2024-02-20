using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;

namespace ACS_Backend.Services;

public class AuthService : IAuthService
{
    private SQL _sql;

    public AuthService(SQL sql)
    {
        _sql = sql;
    }

    public bool IsAuthenticated => throw new NotImplementedException();

    public Personnel CurrentUser => throw new NotImplementedException();

    public async Task<LoginResultModel> Login(LoginResultModel login)
    {
        if (this._sql.Faculties.Any(a => a.Email == login.Email))
        {
            var user = this._sql.Faculties.Single(u => u.Email == login.Email);
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
        }
        throw new FailedLoginException();
    }
}