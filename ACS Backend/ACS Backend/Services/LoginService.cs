using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;

namespace ACS_Backend.Services;

public class LoginService : ILoginService
{
    private SQL _sql;

    public LoginService(SQL sql)
    {
        _sql = sql;
    }

    public async Task<LoginResultModel> Check(LoginModel login)
    {
        if (!_sql.Faculties.Any(x => x.Email == login.Email && x.Password == login.Password))
            throw new ItemNotFoundException();
        else
        {
            var person = _sql.Faculties.Single(x => x.Email == login.Email);
            Console.WriteLine(person.ToString());
            if (!BCrypt.Net.BCrypt.EnhancedVerify(login.Password, person.Password)) throw new FailedLoginException();
                var role = _sql.PersonRoles.Single(x => x.Id == person.RoleId);
            //TODO: implement Token generation
            return new LoginResultModel()
            {
                Email = person.Email,
                Name = person.Name,
                Role = role
            };
        }
    }
}