namespace BackendTest.GuardianService;

[TestClass]
[TestCategory("Services")]
public class DeleteGuardianTest
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
    public async Task DeleteGuardianNoId()
    {
        try
        {
            await _service.DeleteGuardian(Guid.Empty);
            Assert.Fail();

        }
        catch (ArgumentException e)
        {
        }
    }
    [TestMethod]
    public async Task DeleteGuardianBadId()
    {
        try
        {
            await _service.DeleteGuardian(Guid.NewGuid());
            Assert.Fail();

        }
        catch (ItemNotFoundException e)
        {
        }
    }
    [TestMethod]
    public async Task DeleteGuardianGoodId()
    {
        try
        {
            await _service.DeleteGuardian(_guardian.Id);
            

        }
        catch (ItemNotFoundException e)
        {
            Assert.Fail();
        }
    }
    
    
    
}