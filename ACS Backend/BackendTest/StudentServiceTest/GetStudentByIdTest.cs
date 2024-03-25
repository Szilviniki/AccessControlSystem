namespace BackendTest.StudentServiceTest;

[TestClass]
public class GetStudentByIdTest
{
    private static SQL _sql = new SQL();
    private StudentService _studentService = new StudentService(_sql);
    
    [TestMethod]
    public void NoId()
    {
        Assert.ThrowsException<ItemNotFoundException>(()=>_studentService.GetStudent(Guid.Empty));
    }
    [TestMethod]
    public void BadId()
    {
        Assert.ThrowsException<ItemNotFoundException>(()=>_studentService.GetStudent(Guid.NewGuid()));
    }

    [TestMethod]
    public void GoodId()
    {
        var id = Guid.Parse("793C39D1-B2EA-48D4-BA6B-EF79F66B4CC3");
        Assert.AreEqual(_studentService.GetStudent(id).Name, "Test Student");
    }
}