using System.Diagnostics;
using ACS_Backend.Utilities;

namespace BackendTest.StudentServiceTest;

[TestClass]
public class UpdateStudentTest
{
    private static SQL _sql = new();
    private StudentService _studentService = new StudentService(_sql);

    private Student _studentOld = new()
    {
        Name = "Updét Ubul",
        Email = "ubul.update@email.old",
        Phone = "+36903040501",
        CardId = 80085,
        BirthDate = new DateTime(2000, 4, 20),
        ParentId = Guid.Parse("EEB143B1-C5B1-448F-8D78-FFDE83D3091F"),
    };

    private static readonly UpdateStudentModel StudentNew = new UpdateStudentModel()
    {
        Name = "Updét Ursula",
        Email = "ursula.update@email.new",
        Phone = "+36103040500",
        BirthDate = new DateTime(2000, 4, 20),
        ParentId = Guid.Parse("EEB143B1-C5B1-448F-8D78-FFDE83D3091F"),
    };
    
    
    
    private Guid _id = Guid.NewGuid();

    private UpdateStudentModel _naughtyStudent = StudentNew.DeepCopy();

    [TestInitialize]
    public void StudentInit()
    {
        const int cardId = 80085;
        if (!_sql.Students.Any(x => x.CardId == cardId))
        {
            _sql.Students.Add(_studentOld);
            _sql.SaveChanges();
        }

        _id = _sql.Students.First(x => x.CardId == cardId).Id;

        // _naughtyStudent = StudentNew;
    }


    [TestMethod]
    public async Task UpdateStudentSuccess()
    {
        try
        {
            await _studentService.UpdateStudent(StudentNew, _id);
            await _studentService.RemoveStudent(_id);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }


    [TestMethod]
    public async Task UpdateStudentNoId()
    {
        try
        {
            await _studentService.UpdateStudent(_naughtyStudent, Guid.Empty);
            Assert.Fail();
        }
        catch (ItemNotFoundException e)
        {
        }
    }

    [TestMethod]
    public async Task UpdateStudentNoData()
    {
        try
        {
            var stud = new UpdateStudentModel();
            _id = _sql.Students.First(x => x.CardId == 80085).Id;
            await _studentService.UpdateStudent(stud, _id);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task UpdateStudentBadEmail()
    {
        try
        {
            _naughtyStudent.Email = "bademail";
            await _studentService.UpdateStudent(_naughtyStudent, _id);
            Assert.Fail();
        }
        catch (BadFormatException e)
        {
        }
    }

    [TestMethod]
    public async Task UpdateStudentBadPhone()
    {
        try
        {
            _naughtyStudent.Phone = "badphone";
            await _studentService.UpdateStudent(_naughtyStudent, _id);
            Assert.Fail();
        }
        catch (BadFormatException e)
        {
        }
    }

    [TestMethod]
    public async Task UpdateStudentBadParentId()
    {
        try
        {
            _naughtyStudent.Email = "";
            _naughtyStudent = StudentNew.DeepCopy();
            _naughtyStudent.ParentId = Guid.NewGuid();
            await _studentService.UpdateStudent(_naughtyStudent, _id);
            Assert.Fail();
        }
        catch (ReferredEntityNotFoundException e)
        {
        }
    }

    [TestMethod]
    public async Task UpdateStudentUniqueConstraint()
    {
        try
        {
            _naughtyStudent = StudentNew;
            _naughtyStudent.Phone = _sql.Students.First(x => x.CardId > 0).Phone;
            await _studentService.UpdateStudent(_naughtyStudent, _id);
            Assert.Fail();
        }
        catch (UniqueConstraintFailedException<List<string>> e)
        {
        }
    }

    [TestCleanup]
    public void StudentCleanup()
    {
        if (_sql.Students.Any(x => x.CardId == 80085))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.CardId == 80085));
            _sql.SaveChanges();
        }

        _naughtyStudent = StudentNew.DeepCopy();
    }
}