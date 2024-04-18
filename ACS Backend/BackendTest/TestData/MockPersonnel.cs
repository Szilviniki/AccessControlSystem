using DB_Module.Models;
namespace DB_Module.MockData;

public class MockPersonnel : Personnel
{
    public Personnel Faculty = new Personnel()
    {
        Id = Guid.NewGuid(),
        Name = "Teacher 1",
        Phone = "+36400010001",
        Email = "teacher@mock.dat",
        CanLogin = true,
        CardId = 200002,
        Password = "apple",
        Role = 2
    };

    public Personnel Worker = new Personnel()
    {
        Id = Guid.NewGuid(),
        Name = "Worker 1",
        Phone = "+36400010002",
        Email = "worker@mock.dat",
        CanLogin = false,
        CardId = 200003,
        Role = 4
    };


    public Personnel DeepCopyWorker()
    {
        return new Personnel()
        {
            Id = Worker.Id,
            Name = Worker.Name,
            Phone = Worker.Phone,
            Email = Worker.Email,
            CanLogin = Worker.CanLogin,
            CardId = Worker.CardId,
            Role = 4
        };
    }

    public Personnel DeepCopyFaculty()
    {
        return new Personnel()
        {
            Id = Faculty.Id,
            Name = Faculty.Name,
            Phone = Faculty.Phone,
            Email = Faculty.Email,
            CanLogin = Faculty.CanLogin,
            CardId = Faculty.CardId,
            Role = 2
        };
    }

    public UpdatePersonnelModel UpdateFaculty = new()
    {
        Name = "Teacher 1",
        Phone = "+36400010001",
        Email = "teacher@mock.dat",
        Password = "bootom_jeans",
    };
    public UpdatePersonnelModel UpdateWorker = new()
    {
        Name = "Teacher 1",
        Phone = "+36400010001",
        Email = "teacher@mock.dat",
    };

}