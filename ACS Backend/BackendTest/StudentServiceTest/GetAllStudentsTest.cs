﻿namespace BackendTest.StudentServiceTest;

//purpose: Test the GetAllStudents method in the StudentService

[TestClass]
public class GetAllStudentsTest
{
    private static SQL _sql = new SQL();
    private StudentService _studentService = new StudentService(_sql);
    private int _students = _sql.Students.Count();


    [TestMethod]
    public void GetAllStudents()
    {
        var students = _studentService.GetAllStudents();
        Assert.AreEqual(students.Length, _students);
    }
}