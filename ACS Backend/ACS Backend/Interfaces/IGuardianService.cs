namespace ACS_Backend.Interfaces
{
    public interface IGuardianService
    {
        public Task AddGuardian(Guardian guardian);
        public Guardian GetGuardian(Guid id);

        public Array GetAllGuardians();

        public Task UpdateGuardian(Guardian guardian);

        public Task DeleteGuardian(Guid id);
    }
}
