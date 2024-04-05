namespace ACS_Backend.Interfaces
{
    public interface ILockRuleService
    {
        public LockRule GetRestrictionById(int id);
        public Array GetRestrictions();
        public Task CreateRestriction(LockRule lockRule, Guid studentId);
        public Task CreateManyRestrictions(LockRule[] lockRules, Guid studentId);
        public Task DeleteManyRestrictions(int[] ruleIds, Guid studentId);
        public Task DeleteLink(int ruleId, Guid studentId);
        public Task DeleteRestriction(int id);
    }
}
