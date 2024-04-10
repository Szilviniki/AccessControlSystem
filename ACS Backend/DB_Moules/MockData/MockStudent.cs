using DB_Module.Models;

namespace DB_Module.MockData;

public class MockStudent : Student
{
    public Student TestStudent()
    {
        return new Student
        {
            Id = Guid.NewGuid(),
            Name = "Test Student",
            Email = "test.student@test.test",
            Phone = "+36301234566",
            BirthDate = DateTime.Today
        };
    }
}