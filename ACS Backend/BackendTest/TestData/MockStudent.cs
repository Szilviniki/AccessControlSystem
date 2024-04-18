using DB_Module.Models;

namespace DB_Module.MockData;

public class MockStudent : Student
{
    public Student Student = new Student()
    {
        Id = Guid.NewGuid(),
        Name = "Mock Martin",
        Email = "mock.martin@mock.dat",
        Phone = "+36301234566",
        BirthDate = DateTime.Parse("2000-01-01"),
        CardId = 1000001,
        ParentId = Guid.Parse("4a7c7d89-f600-4f6a-9ec8-55602d7a1979")
    };
    
    public Student DeepCopy()
    {
        return new Student()
        {
            Id = Student.Id,
            Name = Student.Name,
            Email = Student.Email,
            Phone = Student.Phone,
            BirthDate = Student.BirthDate,
            CardId = Student.CardId,
            ParentId = Student.ParentId
        };
    }
}