using ACS_Backend.Utilities;

namespace BackendTest.StudentServiceTest;

[TestClass]
public class GetStudentByIdTest
{
    private static SQL _sql = new SQL();

    private StudentService _studentService =
        new StudentService(_sql);


    private Student _student = new()
    {
        Name = "Test Student",
        Email = "test.student@testing.dev",
        Phone = "+36303334567",
        CardId = 546735245,
        ParentId = Guid.Parse("AC80888A-140A-4834-A705-3AF88F132E10"),
        BirthDate = new DateTime(2009, 08, 12).Date
    };

    [TestInitialize]
    public void StudentInit()
    {
        if (_sql.Students.Any(x => x.Email == _student.Email))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.Email == _student.Email));
            _sql.SaveChanges();
        }

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
        Assert.AreEqual(_studentService.GetStudent(id).Name, "Test Student");
    }

    [TestCleanup]
    public async Task StudentCleanup()
    {
        if (_sql.Students.Any(x => x.Email == _student.Email))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.Email == _student.Email));
            await _sql.SaveChangesAsync();
        }
    }
}