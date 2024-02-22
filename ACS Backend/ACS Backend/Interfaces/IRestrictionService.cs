namespace ACS_Backend.Interfaces
{
    public interface IRestrictionService
    {
        public Restriction GetRestrictionById(int id);
        public Array GetRestrictions();
        public Task CreateRestriction(Restriction restriction);
        public Task UpdateRestriction(Restriction restriction);
        public Task DeleteRestriction(int id);
    }
}
