using ACS_Backend.Model;

namespace ACS_Backend.Interfaces
{
    public interface IUniquenessChecker
    {
        public GenericResponseModel<List<string>> IsUniqueStudent(Student student);
        public GenericResponseModel<List<string>> IsUniqueStudentOnUpdate(Student student);
        

        public GenericResponseModel<List<string>> IsUniqueFaculty(Personnel faculty);

        public GenericResponseModel<List<string>> IsUniqueGuardian(Guardian guardian);
    }
}
