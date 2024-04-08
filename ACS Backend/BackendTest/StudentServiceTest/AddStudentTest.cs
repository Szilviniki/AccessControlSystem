// Purpose: Test AddStudent method in StudentService.cs.

using ACS_Backend.Utilities;

namespace BackendTest.StudentServiceTest;

[TestClass]
public class AddStudentTest
{
    private static SQL _sql = new SQL();
    private StudentService _studentService = new StudentService(_sql);
    private int _cardId = 883269;

    [TestInitialize]
    public void StudentInit()
    {
        if (_sql.Students.Any(x => x.CardId == _cardId))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.CardId == _cardId));
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
        catch (NotAddedException e)
        {
        }
    }

    [TestMethod]
    public async Task GoodDataAddStudent()
    {
        try
        {
            if (_sql.Students.Any(x => x.Email == "test.thomas@email.com"||"+36305678943" == x.Phone))
            {
                _sql.Students.Remove(_sql.Students.Single(x =>
                    x.Email == "test.thomas@email.com" || "+36305678943" == x.Phone));
               await _sql.SaveChangesAsync();
            }

            var student = new Student
            {
                Name = "Test Student",
                Email = "test.thomas@email.com",
                Phone = "+36305678943",
                CardId = _cardId,
                ParentId = Guid.Parse("AC80888A-140A-4834-A705-3AF88F132E10"),
                BirthDate = new DateTime(2009, 08, 12).Date
            };
            _cardId = student.CardId;
            await _studentService.AddStudent(student);
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
            var student = new Student
            {
                Name = "Test Student",
                Email = "rossz.email",
                Phone = "+36305678943",
                CardId = new Random().Next(100000, 999999),
                ParentId = Guid.Parse("AC80888A-140A-4834-A705-3AF88F132E10"),
                BirthDate = new DateTime(2009, 08, 12).Date
            };
            await _studentService.AddStudent(student);
            Assert.Fail();
        }
        catch (BadFormatException)
        {
        }
    }

    [TestMethod]
    public async Task BadPhoneAddStudent()
    {
        try
        {
            var student = new Student
            {
                Name = "Test Student",
                Email = "jo.email@adat.teszt",
                Phone = "36305678943",
                CardId = new Random().Next(100000, 999999),
                ParentId = Guid.Parse("AC80888A-140A-4834-A705-3AF88F132E10"),
                BirthDate = new DateTime(2009, 08, 12).Date
            };
            await _studentService.AddStudent(student);
            Assert.Fail();
        }
        catch (BadFormatException)
        {
        }
    }


    [TestMethod]
    public async Task SameCardIdAddStudent()
    {
        try
        {
            var student = new Student
            {
                Name = "Test Student",
                Email = "test.jonathan@email.com",
                Phone = "+36305678944",
                CardId = _cardId,
                ParentId = Guid.Parse("AC80888A-140A-4834-A705-3AF88F132E10"),
                BirthDate = new DateTime(2009, 08, 12).Date
            };
            _sql.Students.Add(student);
            await _sql.SaveChangesAsync();
            await _studentService.AddStudent(student);
            Assert.Fail();
        }
        catch (UniqueConstraintFailedException<List<string>> e)
        {
            foreach (var msg in e.FailedOn)
            {
                Console.Write($" {msg}");
            }
        }
    }

    [TestMethod]
    public async Task BadGuardianIdAddStudent()
    {
        try
        {
            var student = new Student
            {
                Name = "Test Student",
                Email = "test.jonathan@email.com",
                Phone = "+36305678944",
                CardId = _cardId,
                ParentId = Guid.Parse("AC80888A-140A-4834-A705-3AF88F132010"),
                BirthDate = new DateTime(2009, 08, 12).Date
            };
            await _studentService.AddStudent(student);
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
    }
}