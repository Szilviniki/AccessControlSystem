using ACS_Backend.Model;

namespace ACS_Backend.Interfaces;

public interface IStudentService
{
    public Student GetStudent(Guid id);
    public Array GetAllStudents();
    public Task UpdateStudent(UpdateStudentModel student, Guid id);
    public Task RemoveStudent(Guid id);

    public Task AddStudent(Student student);
}