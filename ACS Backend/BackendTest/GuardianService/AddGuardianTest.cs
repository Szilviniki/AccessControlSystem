

namespace BackendTest.GuardianService;

[TestClass]
public class AddGuardianTest
{
    private static Guardian _guardian = new MockGuardian().Parent;
    private static SQL _sql = new SQL();
    private ACS_Backend.Services.GuardianService _parentService = new(_sql, new ObjectValidatorService(), new UniquenessChecker(_sql));

    [TestInitialize]
    public void GuardianInit()
    {
        if (_sql.Parents.Any(x => x.Phone == _guardian.Phone))
        {
            _sql.Parents.Remove(_sql.Parents.Single(x => x.Phone == _guardian.Phone));
        }
    }
    
    [TestCleanup]
    public async Task GuardianCleanup()
    {
        if (_sql.Parents.Any(x => x.Phone == _guardian.Phone))
        {
            _sql.Parents.Remove(_sql.Parents.Single(x => x.Phone == _guardian.Phone));
            await _sql.SaveChangesAsync();
        }
    }

    [TestMethod]
    public async Task AddGuardianNoData()
    {
        try
        {
            await _parentService.AddGuardian(new Guardian());
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task AddGuardianBadEmail()
    {
        try
        {
            var badGuardian = new MockGuardian().DeepCopy();
            badGuardian.Email = "hehehe";
            await _parentService.AddGuardian(badGuardian);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task AddGuardianBadPhone()
    {
        try
        {
            var badGuardian = new MockGuardian().DeepCopy();
            badGuardian.Phone = "hehehe";
            await _parentService.AddGuardian(badGuardian);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task AddGuardianBadPhoneAndEmail()
    {
        try
        {
            var badGuardian = new MockGuardian().DeepCopy();
            badGuardian.Phone = "hehehe";
            badGuardian.Email = "hehehe";
            await _parentService.AddGuardian(badGuardian);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task AddGuardianNoName()
    {
        try
        {
            var badGuardian = new MockGuardian().DeepCopy();
            badGuardian.Name = "";
            await _parentService.AddGuardian(badGuardian);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task AddGuardianNoNameAndEmail()
    {
        try
        {
            var badGuardian = new MockGuardian().DeepCopy();
            badGuardian.Name = "";
            badGuardian.Email = "";
            await _parentService.AddGuardian(badGuardian);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task AddGuardianNoNameAndPhone()
    {
        try
        {
            var badGuardian = new MockGuardian().DeepCopy();
            badGuardian.Name = "";
            badGuardian.Phone = "";
            await _parentService.AddGuardian(badGuardian);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task AddGuardianGoodData()
    {
        try
        {
            await _parentService.AddGuardian(_guardian);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }
    
}