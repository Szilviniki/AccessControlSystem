// Purpose: Test AddStudent method in StudentService.cs.

using DB_Module.MockData;

namespace BackendTest.StudentServiceTest;

[TestClass]
[TestCategory("Services")]
public class AddStudentTest
{
    private static SQL _sql = new SQL();
    private static Student _student = new MockStudent().Student;
    private static Guardian _guardian = new MockGuardian().Parent;
    private StudentService _studentService = new StudentService(_sql);
    private int _cardId = _student.CardId;

    [TestInitialize]
    public void StudentInit()
    {
        if (_sql.Students.Any(x => x.CardId == _cardId))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.CardId == _cardId));
            _sql.SaveChanges();
        }

        if (_sql.Guardians.Any(x=>x.Id == _guardian.Id))
        {
            _sql.Guardians.Remove(_sql.Guardians.Single(x => x.Id == _guardian.Id));
            _sql.Guardians.Add(_guardian);
            _sql.SaveChanges();
        }
    }

    [TestMethod]
    public async Task NoDataAddStudent()
    {
        try
        {
            var student = new Student();
            await _studentService.AddStudent(student);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task GoodDataAddStudent()
    {
        try
        {
            _sql.Guardians.Add(_guardian);
            await _sql.SaveChangesAsync();
            await _studentService.AddStudent(_student);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [TestMethod]
    public async Task BadEmailAddStudent()
    {
        try
        {
            var bad = new MockStudent().DeepCopy();
            bad.Email = "asd@asd";
            await _studentService.AddStudent(bad);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    [TestMethod]
    public async Task BadPhoneAddStudent()
    {
        try
        {
            var bad = new MockStudent().DeepCopy();
            bad.Phone = "asd";
            await _studentService.AddStudent(bad);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }


    [TestMethod]
    public async Task SameCardIdAddStudent()
    {
        try
        {
            var bad = new MockStudent().DeepCopy();
            bad.CardId = _sql.Students.First().CardId;
            await _studentService.AddStudent(bad);
            Assert.Fail();
        }
        catch (UniqueConstraintFailedException<List<string>> e)
        {
        }
    }

    [TestMethod]
    public async Task BadGuardianIdAddStudent()
    {
        try
        {
            var bad = new MockStudent().DeepCopy();
            bad.ParentId = Guid.NewGuid();
            await _studentService.AddStudent(bad);
            Assert.Fail();
        }
        catch (ReferredEntityNotFoundException)
        {
        }
    }

    [TestCleanup]
    public async Task StudentCleanup()
    {
        if (_sql.Students.Any(x => x.CardId == _cardId))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.CardId == _cardId));
            await _sql.SaveChangesAsync();
        }
        if(_sql.Guardians.Any(x=>x.Id== _guardian.Id))
        {
            _sql.Guardians.Remove(_sql.Guardians.Single(x => x.Id == _guardian.Id));
            await _sql.SaveChangesAsync();
        }
    }
}