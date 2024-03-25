namespace BackendTest.StudentServiceTest;

//this is going to hurt to write

[TestClass]
public class UpdateStudentTest
{
    private static SQL _sql = new();
    private StudentService _studentService = new(_sql);

    private Student _studentOld = new()
    {
        Name = "Updét Ubul",
        Email = "ubul.update@email.old",
        Phone = "+36903040501",
        CardId = 80085,
        BirthDate = new DateTime(2000, 4, 20),
        ParentId = Guid.Parse("AC80888A-140A-4834-A705-3AF88F132E10"),
    };

    private static readonly UpdateStudentModel StudentNew = new()
    {
        Name = "Updét Ursula",
        Email = "ursula.update@email.new",
        Phone = "+36103040500",
        BirthDate = new DateTime(2000, 4, 20),
        ParentId = Guid.Parse("AC80888A-140A-4834-A705-3AF88F132E10"),
    };

    private Guid _id = Guid.NewGuid();

    private UpdateStudentModel _naughtyStudent = StudentNew;

    [TestInitialize]
    public void StudentInit()
    {
        const int cardId = 80085;
        if (_sql.Students.Any(x => x.CardId == cardId))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.CardId == cardId));
            _sql.SaveChanges();
        }

        if (_sql.Students.Any(x => x.CardId == cardId))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.CardId == cardId));
            _sql.SaveChanges();
        }
        _sql.Students.Add(_studentOld);
        _sql.SaveChanges();
        _id = _sql.Students.First(x => x.CardId == cardId).Id;
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

    private void ResetnaughtyStudent()
    {
        _naughtyStudent = StudentNew;
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
            _id = _sql.Students.First(x=>x.CardId== 80085).Id;
            await _studentService.UpdateStudent(new UpdateStudentModel(), _id);
            Assert.Fail();
        }
        catch (UnprocessableEntityException e)
        {
        }
    }

    [TestMethod]
    public async Task UpdateStudentBadEmail()
    {
        try
        {
            ResetnaughtyStudent();
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
            ResetnaughtyStudent();
            _naughtyStudent.Phone = "badphone";
            await _studentService.UpdateStudent(_naughtyStudent, _id);
            Assert.Fail();
        }
        catch (BadFormatException e)
        {
        }
    }

    [TestMethod]
    public async Task UpdateStudentEmptyEmail()
    {
        try
        {
            ResetnaughtyStudent();
            _naughtyStudent.Email = "";
            await _studentService.UpdateStudent(_naughtyStudent, _id);
            Assert.Fail();
        }
        catch (UnprocessableEntityException e)
        {
        }
    }

    [TestMethod]
    public async Task UpdateStudentEmptyName()
    {
        try
        {
            ResetnaughtyStudent();
            _naughtyStudent.Name = "";
            await _studentService.UpdateStudent(_naughtyStudent, _id);
            Assert.Fail();
        }
        catch (UnprocessableEntityException e)
        {
        }
    }

    [TestMethod]
    public async Task UpdateStudentEmptyPhone()
    {
        try
        {
            ResetnaughtyStudent();
            _naughtyStudent.Phone = "";
            await _studentService.UpdateStudent(_naughtyStudent, _id);
            Assert.Fail();
        }
        catch (UnprocessableEntityException e)
        {
        }
    }

    [TestMethod]
    public async Task UpdateStudentBadParentId()
    {
        try
        {
            ResetnaughtyStudent();
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
            ResetnaughtyStudent();
            _naughtyStudent.Phone = _sql.Students.First(x=>x.CardId>0).Phone;
            await _studentService.UpdateStudent(_naughtyStudent, _id);
            Assert.Fail();
        }
        catch (UniqueConstraintFailedException<List<string>> e)
        {
        }
    }


}