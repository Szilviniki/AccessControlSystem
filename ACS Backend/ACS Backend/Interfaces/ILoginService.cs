using ACS_Backend.Model;

namespace ACS_Backend.Interfaces;

public interface ILoginService
{
    public Task<LoginResultModel> Check(LoginModel login);
}