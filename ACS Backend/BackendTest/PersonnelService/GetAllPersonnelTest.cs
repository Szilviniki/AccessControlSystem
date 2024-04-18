using ACS_Backend.Interfaces;

namespace BackendTest.PersonnelService;

[TestClass]
[TestCategory("Services")]
public class GetAllPersonnelTest
{
    private static SQL _sql = new SQL();
    private static IEncryptionService _encryptionService = new EncryptionService(_sql);
    private static IObjectValidatorService _objectValidatorService = new ObjectValidatorService();

    private static ACS_Backend.Services.PersonnelService _service = new(_sql, _encryptionService,
        _objectValidatorService);


    [TestMethod]
    public void GetAllPersonnel()
    {
        var everyone = _sql.Personnel.Count();
        Assert.AreEqual(everyone, _service.GetAllFaculties().Length);
        
    }
}