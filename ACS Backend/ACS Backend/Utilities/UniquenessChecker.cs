using ACS_Backend.Model;

namespace ACS_Backend.Utilities;

public class UniquenessChecker
{
    private SQL _sql;

    public UniquenessChecker(SQL sql)
    {
        _sql = sql;
    }

    public GenericResponseModel<List<string>> IsUniqueStudent(Student student)
    {
        var fails = new List<string>();
        if (_sql.Students.Any(x => student.CardId == x.CardId)) fails.Add("Kártyaszám");
        return fails.Count != 0
            ? new GenericResponseModel<List<string>> { Data = fails, QueryIsSuccess = false }
            : new GenericResponseModel<List<string>> { Message = "Minden ok" };
    }

    public GenericResponseModel<List<string>> IsUniqueGroup(Group group)
    {
        var fails = new List<string>();
        if (_sql.Groups.Any(x => group.Name == x.Name)) fails.Add("Név");
        return fails.Count != 0
            ? new GenericResponseModel<List<string>> { Data = fails, QueryIsSuccess = false }
            : new GenericResponseModel<List<string>> { Message = "Minden ok" };
    }

    public GenericResponseModel<List<string>> IsUniqueRole(Role role)
    {
        var fails = new List<string>();
        if (_sql.PersonRoles.Any(x => role.Name == x.Name)) fails.Add("Név");
        return fails.Count != 0
            ? new GenericResponseModel<List<string>> { Data = fails, QueryIsSuccess = false }
            : new GenericResponseModel<List<string>> { Message = "Minden ok" };
    }

    public GenericResponseModel<List<string>> IsUniqueFaculty(Personnel faculty)
    {
        var fails = new List<string>();
        if (_sql.Faculties.Any(x => faculty.CardId == x.CardId)) fails.Add("Kártyaszám");
        if (_sql.Faculties.Any(x => x.Email == faculty.Email)) fails.Add("Email cím");
        return fails.Count != 0
            ? new GenericResponseModel<List<string>> { Data = fails, QueryIsSuccess = false }
            : new GenericResponseModel<List<string>> { Message = "Minden ok" };
    }
}