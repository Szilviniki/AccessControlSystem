using ACS_Backend.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ACS_Backend.Interfaces;

namespace BackendTest.PersonnelService;

[TestClass]
[TestCategory("Services")]
public class UpdateFacultyTest
{
    private static SQL _sql = new SQL();
    private static IEncryptionService _encryptionService = new EncryptionService(_sql);
    private static IObjectValidatorService _objectValidatorService = new ObjectValidatorService();
    private static ACS_Backend.Services.PersonnelService _service = new ACS_Backend.Services.PersonnelService(_sql, _encryptionService, _objectValidatorService);
    private static MockPersonnel _mock = new MockPersonnel();
    private Personnel _faculty = _mock.DeepCopyFaculty();
    private UpdatePersonnelModel _updateFaculty = _mock.UpdateFaculty;

    [TestInitialize]
    public async Task FacultyInit()
    {
        if (!_sql.Personnel.Any(x => x.Id == _faculty.Id))
        {
            _sql.Personnel.Add(_faculty);
            await _sql.SaveChangesAsync();
        }
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        if (_sql.Personnel.Any(x => x.Id == _faculty.Id))
        {
            _sql.Personnel.Remove(_sql.Personnel.Single(x => x.Id == _faculty.Id));
            await _sql.SaveChangesAsync();
        }
    }

    [TestMethod]
    public async Task UpdateFacultySuccess()
    {

        await _service.UpdateFaculty(_updateFaculty, _faculty.Id);

        var updatedFaculty = _sql.Personnel.Single(x => x.Id == _faculty.Id);
        Assert.AreEqual(_updateFaculty.Name, updatedFaculty.Name);
        Assert.AreEqual(_updateFaculty.Email, updatedFaculty.Email);
    }

    [TestMethod]
    public async Task UpdateFacultyNoId()
    {
        try
        {
            await _service.UpdateFaculty(_updateFaculty, Guid.Empty);
            Assert.Fail();
        }
        catch (ItemNotFoundException)
        {
        }
    }

    [TestMethod]
    public async Task UpdateFacultyBadId()
    {
        try
        {
            await _service.UpdateFaculty(_updateFaculty, Guid.NewGuid());
            Assert.Fail();
        }
        catch (ItemNotFoundException)
        {
        }
    }

    [TestMethod]
    public async Task UpdateFacultyBadData()
    {
        try
        {
            _updateFaculty.Email = "bad";
            await _service.UpdateFaculty(_updateFaculty, _faculty.Id);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }
}