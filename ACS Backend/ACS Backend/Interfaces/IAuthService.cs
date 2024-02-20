using ACS_Backend.Model;

namespace ACS_Backend.Interfaces;

public interface IAuthService
{
    bool IsAuthenticated { get; }
    Faculty CurrentUser { get; }
    public Task<LoginResultModel> Login(LoginModel login);
}