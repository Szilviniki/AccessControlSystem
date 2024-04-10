using System.Security.Cryptography.X509Certificates;
using DB_Module.Models;

namespace DB_Module.MockData;

public class MockParent : Guardian
{
    public Guardian Parent = new Guardian()
    {
        Id = Guid.Parse("4a7c7d89-f600-4f6a-9ec8-55602d7a1979"),
        Name = "Parent1",
        Phone = "+36301234567",
        Email = "mock.mark@mockify.eu"
    };
}