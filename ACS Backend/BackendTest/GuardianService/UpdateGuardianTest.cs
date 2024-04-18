namespace BackendTest.GuardianService;

[TestClass]
[TestCategory("Services")]
public class UpdateGuardianTest
{
    private static Guardian _guardian = new MockGuardian().Parent;
    private static SQL _sql = new SQL();

    private ACS_Backend.Services.GuardianService _service = new(_sql, new ObjectValidatorService(),
        new UniquenessChecker(_sql));


    [TestInitialize]
    public async Task GuardianInit()
    {
        if (_sql.Guardians.Any(x => x.Phone == _guardian.Phone))
        {
            _sql.Guardians.Remove(_sql.Guardians.Single(x => x.Phone == _guardian.Phone));
            await _sql.SaveChangesAsync();
        }

        _sql.Guardians.Add(_guardian);
        await _sql.SaveChangesAsync();
    }

    [TestCleanup]
    public async Task GuradianCleanup()
    {
        if (_sql.Guardians.Any(x => x.Id == _guardian.Id))
        {
            _sql.Guardians.Remove(_sql.Guardians.Single(x => x.Id == _guardian.Id));
            await _sql.SaveChangesAsync();
        }
    }

    [TestMethod]
    public async Task UpdateGuardianNoData()
    {
        try
        {
            await _service.UpdateGuardian(new Guardian(), _guardian.Id);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    [TestMethod]
    public async Task UpdateGuardianNoId()
    {
        try
        {
            await _service.UpdateGuardian(_guardian, Guid.Empty);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    [TestMethod]
    public async Task UpdateGuardianBadId()
    {
        try
        {
            await _service.UpdateGuardian(_guardian, Guid.NewGuid());
            Assert.Fail();
        }
        catch (ItemNotFoundException)
        {
        }
    }

    [TestMethod]
    public async Task UpdateGuardianBadEmail()
    {
        try
        {
            var baddie = new MockGuardian().DeepCopy();
            baddie.Email = "mwuhahahaha";

            await _service.UpdateGuardian(baddie, _guardian.Id);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    [TestMethod]
    public async Task UpdateGuardianBadPhone()
    {
        try
        {
            var baddie = new MockGuardian().DeepCopy();
            baddie.Phone = "mwuhahahaha";

            await _service.UpdateGuardian(baddie, _guardian.Id);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }
    [TestMethod]
    public async Task UpdateGuardianGoodData()
    {
        try
        {
            var baddie = new MockGuardian().DeepCopy();
            baddie.Name = "Updated Ursula";

            await _service.UpdateGuardian(baddie, _guardian.Id);
            
        }
        catch (Exception)
        {
            Assert.Fail();
        }
    }
    
}