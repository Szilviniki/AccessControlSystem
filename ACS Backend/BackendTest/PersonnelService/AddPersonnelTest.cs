namespace BackendTest.PersonnelService;

using ACS_Backend.Services;

[TestClass]
public class AddPersonnelTest
{
    private static SQL _sql = new SQL();
    private static PersonnelService _service = new PersonnelService(_sql, new EncryptionService(_sql));
    private static MockPersonnel _mock = new MockPersonnel();
    private Personnel _worker = _mock.Worker;
    private Personnel _faculty = _mock.Faculty;
    private Role _role = new Role();

    [TestInitialize]
    public async Task PersonnelInit()
    {
        if (_sql.Personnel.Any(x => x.Phone == _worker.Phone))
        {
            _sql.Personnel.Remove(_sql.Personnel.Single(x => x.Phone == _worker.Phone));
            await _sql.SaveChangesAsync();
        }

        if (_sql.Personnel.Any(x => x.Phone == _faculty.Phone))
        {
            _sql.Personnel.Remove(_sql.Personnel.Single(x => x.Phone == _faculty.Phone));
            await _sql.SaveChangesAsync();
        }

        if (_sql.PersonRoles.Any(x => x.Name == _role.Name))
        {
            _sql.PersonRoles.Remove(_sql.PersonRoles.Single(x => x.Name == _role.Name));
            await _sql.SaveChangesAsync();
        }

        _sql.PersonRoles.Add(_role);
        await _sql.SaveChangesAsync();
        _worker.RoleId = _sql.PersonRoles.Find(_role.Name).Id;
        _faculty.RoleId = _sql.PersonRoles.Find(_role.Name).Id;
    }

    [TestCleanup]
    public async Task PersonnelCleanup()
    {
        if (_sql.Personnel.Any(x => x.Phone == _worker.Phone))
        {
            _sql.Personnel.Remove(_sql.Personnel.Single(x => x.Phone == _worker.Phone));
            await _sql.SaveChangesAsync();
        }

        if (_sql.Personnel.Any(x => x.Phone == _faculty.Phone))
        {
            _sql.Personnel.Remove(_sql.Personnel.Single(x => x.Phone == _faculty.Phone));
            await _sql.SaveChangesAsync();
        }

        if (_sql.PersonRoles.Any(x => x.Name == _role.Name))
        {
            _sql.PersonRoles.Remove(_sql.PersonRoles.Single(x => x.Name == _role.Name));
            await _sql.SaveChangesAsync();
        }
    }

    [TestMethod]
    public async Task AddPersonnelNoData()
    {
        try
        {
            await _service.AddFaculty(new Personnel());
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    [TestMethod]
    public async Task AddPersonnelNoRole()
    {
        try
        {
            var v = new MockPersonnel().Worker;
            v.RoleId = 0;
            await _service.AddFaculty(_worker);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    [TestMethod]
    public async Task AddPersonnelBadEmail()
    {
        try
        {
            var v = _mock.DeepCopyWorker();
            v.Email = "asd@asd";
            await _service.AddFaculty(v);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }
}