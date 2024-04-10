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
        RoleId = 99
    };

    public Personnel Worker = new Personnel()
    {
        Id = Guid.NewGuid(),
        Name = "Worker 1",
        Phone = "+36400010002",
        Email = "worker@mock.dat",
        CanLogin = false,
        CardId = 200003,
        RoleId = 99
    };
    
    public Role MockRole = new Role()
    {
        Name = "MockRole"
    };
}