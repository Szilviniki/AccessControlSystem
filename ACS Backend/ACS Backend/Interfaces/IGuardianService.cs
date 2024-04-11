namespace ACS_Backend.Interfaces
{
    public interface IGuardianService
    {
        public Task AddGuardian(Guardian guardian);
        public Guardian GetGuardian(Guid id);

        public Array GetAllGuardians();

        public Task UpdateGuardian(Guardian guardian, Guid id);

        public Task DeleteGuardian(Guid id);
    }
}
