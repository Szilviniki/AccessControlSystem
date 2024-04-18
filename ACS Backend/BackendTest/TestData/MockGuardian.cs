using DB_Module.Models;

namespace DB_Module.MockData;

public class MockGuardian : Guardian
{
    public Guardian Parent = new Guardian
    {
        Id = Guid.Parse("4a7c7d89-f600-4f6a-9ec8-55602d7a1979"),
        Name = "Parent1",
        Phone = "+36301234567",
        Email = "mock.mark@mockify.eu"
    };

    public Guardian DeepCopy()
    {
        return new Guardian
        {
            Id = Parent.Id,
            Name = Parent.Name,
            Phone = Parent.Phone,
            Email = Parent.Email
        };
    }
}