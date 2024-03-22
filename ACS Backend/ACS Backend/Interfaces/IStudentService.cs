namespace ACS_Backend.Interfaces;

public interface IStudentService
{
    public Student GetStudent(Guid id);
    public Array GetAllStudents();
    public Task UpdateStudent(Student student);
    public Task RemoveStudent(Guid id);

    public Task AddStudent(Student student);
    public Array GetExtendedStudent(int cardId);
    public Array GetAllExtendedStudents();
}