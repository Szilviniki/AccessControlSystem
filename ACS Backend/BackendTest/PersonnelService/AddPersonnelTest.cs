namespace BackendTest.PersonnelService;

using ACS_Backend.Services;

[TestClass]
public class AddPersonnelTest
{
    private static SQL _sql = new SQL();

    private static PersonnelService _service =
        new PersonnelService(_sql, new EncryptionService(_sql), new ObjectValidatorService());

    private static MockPersonnel _mock = new MockPersonnel();
    private Personnel _worker = _mock.Worker;
    private Personnel _faculty = _mock.Faculty;
    private Role _role = new MockPersonnel().MockRole;

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
        _worker.RoleId = _sql.PersonRoles.First(x => x.Name == _role.Name).Id;
        _faculty.RoleId = _sql.PersonRoles.First(x => x.Name == _role.Name).Id;
        
    }

    
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
    public async Task AddPersonnelFacultyNoRoleId()
    {
        try
        {
            var v = new MockPersonnel().DeepCopyFaculty();
            v.RoleId = 0;
            await _service.AddFaculty(_worker);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    [TestMethod]
    public async Task AddPersonnelFacultyBadEmail()
    {
        try
        {
            var v = _mock.DeepCopyFaculty();
            v.Email = "asd@asd";
            await _service.AddFaculty(v);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    [TestMethod]
    public async Task AddPersonnelFacultyBadPhone()
    {
        try
        {
            var v = _mock.DeepCopyFaculty();
            v.Phone = "asd@asd";
            await _service.AddFaculty(v);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    [TestMethod]
    public async Task AddPersonnelFacultyBadPassword()
    {
        try
        {
            var v = _mock.DeepCopyFaculty();
            v.Password = "";
            await _service.AddFaculty(v);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task AddPersonnelFaculty()
    {
        try
        {
            await _service.AddFaculty(_faculty);
            Assert.IsTrue(_sql.Personnel.Any(x => x.Phone == _faculty.Phone));
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
    }
    
    [TestMethod]
    public async Task TaskAddPersonnelFacultyTwice()
    {
        try
        {
            await _service.AddFaculty(_faculty);
            await _service.AddFaculty(_faculty);
            Assert.Fail();
        }
        catch (UniqueConstraintFailedException<List<string>> e)
        {
            Assert.IsTrue(e.FailedOn.Contains("Kártyaszám"));
        }
    }
}