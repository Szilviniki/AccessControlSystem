using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.IdentityModel.Tokens;

namespace ACS_Backend.Utilities;

public class UniquenessChecker : IUniquenessChecker
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
        if (_sql.Students.Any(x => x.Phone == student.Phone)) fails.Add("Telefonszám");
        if (_sql.Personnel.Any(x => x.CardId == student.CardId) || _sql.Students.Any(x => x.CardId == student.CardId))
            fails.Add("Kártyaszám");
        return fails.Count != 0
            ? new GenericResponseModel<List<string>> { Data = fails, QueryIsSuccess = false }
            : new GenericResponseModel<List<string>> { Message = "Minden ok" };
    }


    public GenericResponseModel<List<string>> IsUniqueStudentOnUpdate(Student student)
    {
        var fails = new List<string>();

        var oldStudent = _sql.Students.Find(student.Id);
        
        if (!student.Phone.IsNullOrEmpty()&&student.Phone!=oldStudent.Phone)
        {
            if (_sql.Students.Any(x => x.Phone == student.Phone)) fails.Add("Telefonszám");
        }

        if (!student.Email.IsNullOrEmpty()&&oldStudent.Email!=student.Email)
        {
            if(_sql.Students.Any(x=>x.Email == student.Email)) fails.Add("Email cím");
        }

        if (!student.Name.IsNullOrEmpty()&&oldStudent.Name!=student.Name)
        {
            if (_sql.Students.Any(x => x.Name == student.Name)) fails.Add("Név");
        }

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
        if (_sql.Personnel.Any(x => faculty.CardId == x.CardId)) fails.Add("Kártyaszám");
        if (_sql.Personnel.Any(x => x.Email == faculty.Email)) fails.Add("Email cím");
        return fails.Count != 0
            ? new GenericResponseModel<List<string>> { Data = fails, QueryIsSuccess = false }
            : new GenericResponseModel<List<string>> { Message = "Minden ok" };
    }

    public GenericResponseModel<List<string>> IsUniqueGuardian(Guardian guardian)
    {
        var fails = new List<string>();
        if (_sql.Parents.Any(x => x.Name == guardian.Name)) fails.Add("Név");
        if (_sql.Parents.Any(x => guardian.Phone == x.Phone)) fails.Add("Telefonszám");
        if (_sql.Parents.Any(x => x.Email == guardian.Email)) fails.Add("Email cím");
        return fails.Count != 0
            ? new GenericResponseModel<List<string>> { Data = fails, QueryIsSuccess = false }
            : new GenericResponseModel<List<string>> { Message = "Minden ok" };
    }
}