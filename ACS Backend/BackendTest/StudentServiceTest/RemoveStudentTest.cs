namespace BackendTest.StudentServiceTest;

[TestClass]
public class RemoveStudentTest
{
    private static SQL _sql = new SQL();
    private StudentService _studentService = new StudentService(_sql);
    private Guid? StudentId;

    private Student _student = new Student
    {
        Name = "Törlendő Tamra",
        Email = "torcsi.tamcsi@deletepls.com",
        Phone = "+36301234567",
        CardId = 4206900,
        BirthDate = DateTime.Now,
        ParentId = Guid.Parse("AC80888A-140A-4834-A705-3AF88F132E10"),
    };


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

    [TestMethod]
    public async Task RemoveStudentGoodId()
    {
        if (!_sql.Students.Any(student => student.CardId == _student.CardId))
        {
            _sql.Students.Add(_student);
            await _sql.SaveChangesAsync();
        }

        StudentId = _sql.Students.First(x => x.CardId == _student.CardId).Id;
        await _studentService.RemoveStudent(StudentId.Value);
        Assert.IsFalse(_sql.Students.Any(x => x.Id == StudentId));
    }
}