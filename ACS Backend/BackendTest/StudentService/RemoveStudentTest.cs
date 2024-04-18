using DB_Module.MockData;

namespace BackendTest.StudentServiceTest;

[TestClass]
public class RemoveStudentTest
{
    private static SQL _sql = new SQL();
    private StudentService _studentService = new StudentService(_sql);
    private static Student _student = new MockStudent().Student;
    private static Guardian _guardian = new MockGuardian().Parent;

    [TestInitialize]
    public void StudentInit()
    {
        if (_sql.Students.Any(x => x.Email == _student.Email))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.Email == _student.Email));
        }

        if (_sql.Guardians.Any(x => x.Id == _guardian.Id))
        {
            _sql.Guardians.Remove(_sql.Guardians.Single(x => x.Id == _guardian.Id));
        }

        _sql.Guardians.Add(_guardian);
        _sql.Students.Add(_student);
        _sql.SaveChanges();
    }


    [TestMethod]
    public async Task RemoveStudentNoId()
    {
        try
        {
            await _studentService.RemoveStudent(Guid.Empty);
            Assert.Fail();
        }
        catch (ItemNotFoundException e)
        {
        }
    }

    [TestMethod]
    public async Task RemoveStudentBadId()
    {
        try
        {
            await _studentService.RemoveStudent(Guid.NewGuid());
            Assert.Fail();
        }
        catch (ItemNotFoundException e)
        {
        }
    }

    [TestCleanup]
    public async Task StudentCleanup()
    {
        if (_sql.Students.Any(x => x.Email == _student.Email))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.Email == _student.Email));
            await _sql.SaveChangesAsync();
        }

        if (_sql.Guardians.Any(x => x.Id == _guardian.Id))
        {
            _sql.Guardians.Remove(_sql.Guardians.Single(x => x.Id == _guardian.Id));
            await _sql.SaveChangesAsync();
        }
    }
}