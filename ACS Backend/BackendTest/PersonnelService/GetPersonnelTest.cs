using ACS_Backend.Interfaces;

namespace BackendTest.PersonnelService;

[TestClass]
[TestCategory("Services")]
public class GetPersonnelTest
{
    private static SQL _sql = new SQL();
    private static IEncryptionService _encryptionService = new EncryptionService(_sql);
    private static IObjectValidatorService _objectValidatorService = new ObjectValidatorService();

    private static ACS_Backend.Services.PersonnelService _service = new(_sql, _encryptionService,
        _objectValidatorService);

    private static MockPersonnel _mock = new MockPersonnel();
    private Personnel _faculty = _mock.DeepCopyFaculty();

    [TestInitialize]
    public async Task Init()
    {
        if (!_sql.Personnel.Any(x => x.CardId == _faculty.CardId))
        {
            _sql.Personnel.Add(_faculty);
            await _sql.SaveChangesAsync();
        }
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        if (_sql.Personnel.Any(x => x.CardId == _faculty.CardId))
        {
            _sql.Personnel.Remove(_sql.Personnel.Single(x => x.CardId == _faculty.CardId));
            await _sql.SaveChangesAsync();
        }
    }

    [TestMethod]
    public void GetPersonnelSuccess()
    {
        var personnel = _service.GetFaculty(_faculty.Id);
        Assert.AreEqual(_faculty.Name, personnel.Name);
        Assert.AreEqual(_faculty.Email, personnel.Email);
    }

    [TestMethod]
    public void GetPersonnelNoId()
    {
        try
        {
            _service.GetFaculty(Guid.Empty);
            Assert.Fail();
        }
        catch (ItemNotFoundException)
        {
        }
    }

    [TestMethod]
    public void GetPersonnelRandomId()
    {
        try
        {
            _service.GetFaculty(Guid.NewGuid());
            Assert.Fail();
        }
        catch (ItemNotFoundException e)
        {
        }
    }
}