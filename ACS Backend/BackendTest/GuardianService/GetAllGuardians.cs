namespace BackendTest.GuardianService;

[TestClass]
public class GetAllGuardians
{
    private static SQL _sql = new SQL();

    private ACS_Backend.Services.GuardianService _parentService =
        new(_sql, new ObjectValidatorService(), new UniquenessChecker(_sql));

    [TestMethod]
    public void GetAllGuardiansTest()
    {
        var result = _parentService.GetAllGuardians().Length;
        var expected = _sql.Parents.Count();
        Assert.AreEqual(expected, result);
    }
}