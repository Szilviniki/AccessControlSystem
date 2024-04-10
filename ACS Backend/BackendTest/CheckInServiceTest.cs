using DB_Module.MockData;

namespace BackendTest;

[TestClass]
public class CheckInServiceTest
{
    private static Student _student = new MockStudent().Student;
    private static Guardian _guardian = new MockParent().Parent;
    private static Personnel _worker = new MockPersonnel().Worker;
    private static Role _role = new MockPersonnel().MockRole;
    private static SQL _sql = new SQL();
    private CheckInService _checkInService = new(_sql);

    [TestInitialize]
    public void Setup()
    {
        _role.Id = 0;

        if (_sql.Personnel.Any(x => x.CardId == _worker.CardId))
        {
            _sql.Personnel.Remove(_sql.Personnel.Single(x => x.CardId == _worker.CardId));
        }

        if (_sql.PersonRoles.Any(x => x.Name == _role.Name))
        {
            var temp = _sql.PersonRoles.Single(x => x.Name == _role.Name);
            _sql.PersonRoles.Remove(temp);
        }

        if (_sql.Students.Any(x => x.CardId == _student.CardId))
        {
            _sql.Students.Remove(_sql.Students.Single(x => x.CardId == _student.CardId));
        }

        if (_sql.Parents.Any(x => x.Email == _guardian.Email))
        {
            _sql.Parents.Remove(_sql.Parents.Single(x => x.Email == _guardian.Email));
        }

        _sql.PersonRoles.Add(_role);
        _sql.SaveChanges();

        var role = _sql.PersonRoles.FirstOrDefault(x => x.Name == _role.Name);
        if (role != null)
        {
            _worker.RoleId = role.Id;
            _role.Id = role.Id;
        }

        _sql.Personnel.Add(_worker);

        _sql.Parents.Add(_guardian);
        _sql.Students.Add(_student);

        _sql.SaveChanges();
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

        var role = _sql.PersonRoles.FirstOrDefault(x => x.Id == _role.Id);
        if (role != null)
        {
            _sql.PersonRoles.Remove(role);
            await _sql.SaveChangesAsync();
        }

        var guardian = _sql.Parents.FirstOrDefault(x => x.Email == _guardian.Email);
        if (guardian != null)
        {
            _sql.Parents.Remove(guardian);
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