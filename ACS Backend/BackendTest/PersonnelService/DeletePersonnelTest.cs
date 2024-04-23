using ACS_Backend.Services;

namespace BackendTest.PersonnelService;

[TestClass]
[TestCategory("Services")]
public class DeletePersonnelTest
{
    private static SQL _sql = new SQL();

    private static ACS_Backend.Services.PersonnelService _service =
        new(_sql, new EncryptionService(_sql), new ObjectValidatorService());

    private static MockPersonnel _mock = new MockPersonnel();
    private Personnel _worker = _mock.Worker;
    private Personnel _faculty = _mock.Faculty;

    [TestInitialize]
    public async Task PersonnelInit()
    {
        if (!_sql.Personnel.Any(x => x.CardId == _worker.CardId))
        {
            _sql.Personnel.Add(_worker);
            await _sql.SaveChangesAsync();
        }

        if (!_sql.Personnel.Any(x => x.CardId == _faculty.CardId))
        {
            _sql.Personnel.Add(_faculty);
            await _sql.SaveChangesAsync();
        }
    }

    [TestCleanup]
    public async Task Cleanup()
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
    }

    [TestMethod]
    [TestCategory("Services")]
    public async Task DeletePersonnelNoId()
    {
        try
        {
            await _service.RemoveFaculty(Guid.Empty);
            Assert.Fail();
        }
        catch (ItemNotFoundException)
        {
        }
    }

    [TestMethod]
    public async Task DeletePersonnelBadId()
    {
        try
        {
            await _service.RemoveFaculty(Guid.NewGuid());
            Assert.Fail();
        }
        catch (ItemNotFoundException)
        {
        }
    }

    [TestMethod]
    public async Task DeletePersonnelGoodId()
    {
        try
        {
            await _service.RemoveFaculty(_faculty.Id);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }
}