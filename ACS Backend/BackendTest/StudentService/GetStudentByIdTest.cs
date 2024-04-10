using ACS_Backend.Utilities;
using DB_Module.MockData;

namespace BackendTest.StudentServiceTest;

[TestClass]
public class GetStudentByIdTest
{
    private static SQL _sql = new SQL();

    private StudentService _studentService = new(_sql);
    private static Student _student = new MockStudent().Student;
    private static Guardian _guardian = new MockGuardian().Parent;

    [TestInitialize]
    public void StudentInit()
    {
        if (_sql.Students.Any(x => x.Email == _student.Email))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.Email == _student.Email));
            
        }
        if(_sql.Parents.Any(x => x.Id == _guardian.Id))
        {
            _sql.Parents.Remove(_sql.Parents.Single(x => x.Id == _guardian.Id));
            
        }
        _sql.Parents.Add(_guardian);
        _sql.Students.Add(_student);
        _sql.SaveChanges();
    }

    [TestMethod]
    public void NoId()
    {
        Assert.ThrowsException<ItemNotFoundException>(() => _studentService.GetStudent(Guid.Empty));
    }

    [TestMethod]
    public void BadId()
    {
        Assert.ThrowsException<ItemNotFoundException>(() => _studentService.GetStudent(Guid.NewGuid()));
    }

    [TestMethod]
    public void GoodId()
    {
        var id = _student.Id;
        Assert.AreEqual(_studentService.GetStudent(id).Name, _student.Name);
    }

    [TestCleanup]
    public async Task StudentCleanup()
    {
        if (_sql.Students.Any(x => x.Email == _student.Email))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.Email == _student.Email));
            await _sql.SaveChangesAsync();
        }

        if (_sql.Parents.Any(x => x.Id == _guardian.Id))
        {
            _sql.Parents.Remove(_sql.Parents.Single(x => x.Id == _guardian.Id));
            await _sql.SaveChangesAsync();
        }
    }
}