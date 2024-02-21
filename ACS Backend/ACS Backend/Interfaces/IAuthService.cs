using ACS_Backend.Model;

namespace ACS_Backend.Interfaces;

public interface IAuthService
{
    bool IsAuthenticated { get; }
    Personnel CurrentUser { get; }
    public Task<LoginResultModel> Login(LoginModel login);
}