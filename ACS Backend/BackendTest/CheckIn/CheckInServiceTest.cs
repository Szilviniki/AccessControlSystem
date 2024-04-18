using DB_Module.MockData;

namespace BackendTest.CheckIn;

[TestClass]
public class CheckInServiceTest
{
    private static Student _student = new MockStudent().Student;
    private static Guardian _guardian = new MockGuardian().Parent;
    private static Personnel _worker = new MockPersonnel().Worker;
    private static SQL _sql = new SQL();
    private CheckInService _checkInService = new(_sql);

    [TestInitialize]
    public async Task Setup()
    {


        if (!_sql.Personnel.Any(x => x.CardId == _worker.CardId))
        {
            _sql.Personnel.Add(_worker);
        }
        
        if (!_sql.Guardians.Any(x => x.Id == _guardian.Id))
        {
            _sql.Guardians.AddAsync(_guardian);
            await _sql.SaveChangesAsync();
        }

        if (!_sql.Students.Any(x => x.Email == _student.Email))
        {
            _student.ParentId = _guardian.Id;
            _sql.Students.Add(_student);
        }

        await _sql.SaveChangesAsync();
    }

    [TestMethod]
    public async Task CheckInStudentBadId()
    {
        try
        {
            await _checkInService.CheckStudent(0);
            Assert.Fail();
        }
        catch (ItemNotFoundException e)
        {
        }
    }

    [TestMethod]
    public async Task CheckInPersonnelBadId()
    {
        try
        {
            await _checkInService.CheckPersonnel(0);
            Assert.Fail();
        }
        catch (ItemNotFoundException e)
        {
        }
    }

    [TestMethod]
    public async Task CheckInStudentGoodId()
    {
        try
        {
            await _checkInService.CheckStudent(_student.CardId);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [TestMethod]
    public async Task CheckInPersonnelGoodId()
    {
        try
        {
            // Console.WriteLine(_sql.Personnel.Any(x=>x.CardId == _worker.CardId)? "present":"not present");
            await _checkInService.CheckPersonnel(_worker.CardId);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        var worker = _sql.Personnel.FirstOrDefault(x => x.CardId == _worker.CardId);
        if (worker != null)
        {
            _sql.Personnel.Remove(worker);
            await _sql.SaveChangesAsync();
        }

        var guardian = _sql.Guardians.FirstOrDefault(x => x.Email == _guardian.Email);
        if (guardian != null)
        {
            _sql.Guardians.Remove(guardian);
            await _sql.SaveChangesAsync();
        }

        var student = _sql.Students.FirstOrDefault(x => x.Email == _student.Email);
        if (student != null)
        {
            _sql.Students.Remove(student);
            await _sql.SaveChangesAsync();
        }
    }
}