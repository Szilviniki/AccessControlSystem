﻿using ACS_Backend.Utilities;

namespace BackendTest.StudentServiceTest;

//purpose: Test the GetAllStudents method in the StudentService

[TestClass]
public class GetAllStudentsTest
{
    private static SQL _sql = new SQL();
    private StudentService _studentService = new StudentService(_sql, new UniquenessChecker(_sql), new MatchingService());
    private int _students = _sql.Students.Count();


    [TestMethod]
    public void GetAllStudents()
    {
        var students = _studentService.GetAllStudents();
        Assert.AreEqual(students.Length, _students);
    }
}