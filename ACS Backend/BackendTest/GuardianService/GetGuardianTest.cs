namespace BackendTest.GuardianService;

[TestClass]
public class GetGuardianTest
{
    private static Guardian _guardian = new MockGuardian().Parent;
    private static SQL _sql = new SQL();

    private ACS_Backend.Services.GuardianService _parentService =
        new(_sql, new ObjectValidatorService(), new UniquenessChecker(_sql));


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
    public async Task GuardianCleanup()
    {
        if (_sql.Guardians.Any(x => x.Phone == _guardian.Phone))
        {
            _sql.Guardians.Remove(_sql.Guardians.Single(x => x.Phone == _guardian.Phone));
            await _sql.SaveChangesAsync();
        }
    }
    
    [TestMethod]
    public void GetGuardianGoodId()
    {
        try
        {
            var guardian = _parentService.GetGuardian(_guardian.Id);
            Assert.AreEqual(_guardian, guardian);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }
    
    [TestMethod]
    public void GetGuardianBadId()
    {
        try
        {
            _parentService.GetGuardian(Guid.NewGuid());
            Assert.Fail();
        }
        catch (ItemNotFoundException e)
        {
        }
    }
    [TestMethod]
    public void GetGuardianNoId()
    {
        try
        {
            _parentService.GetGuardian(Guid.Empty);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }
    
    
}