using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ACS_Backend.Services
{
    public class LockRuleService : ILockRuleService
    {
        private SQL _sql;

        public LockRuleService(SQL sql)
        {
            _sql = sql;
        }

        public async Task DeleteManyRestrictions(int[] ruleIds, Guid studentId)
        {
            await using var transaction = await _sql.Database.BeginTransactionAsync();

            try
            {
                var linksToDelete =
                    _sql.StudentsLockRules.Where(x => ruleIds.Contains(x.LockRuleId) && x.StudentId == studentId);
                _sql.StudentsLockRules.RemoveRange(linksToDelete);


                var rulesToDelete = _sql.LockRules.Where(x => ruleIds.Contains(x.LockRuleId));
                _sql.LockRules.RemoveRange(rulesToDelete);

                await _sql.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteLink(int ruleId, Guid studentId)
        {
            if (_sql.StudentsLockRules.Any(x => x.LockRuleId == ruleId && x.StudentId == studentId))
            {
                var pair = _sql.StudentsLockRules.Single(x => x.LockRuleId == ruleId && x.StudentId == studentId);
                _sql.Remove(pair);
                await _sql.SaveChangesAsync();
                return;
            }

            throw new ItemNotFoundException();
        }

        public async Task DeleteRestriction(int id)
        {
            if (_sql.LockRules.Any(x => x.LockRuleId == id))
            {
                _sql.Remove(_sql.LockRules.Single(x => x.LockRuleId == id));
                await _sql.SaveChangesAsync();
            }
            else
            {
                throw new ItemNotFoundException();
            }
        }

        public LockRule GetRestrictionById(int id)
        {
            if (!_sql.LockRules.Any(x => x.LockRuleId == id)) throw new ItemNotFoundException();
            return _sql.LockRules.Single(x => x.LockRuleId == id);
        }

        public Array GetRestrictions()
        {
            return _sql.LockRules.ToArray();
        }

        public async Task CreateRestriction(LockRule lockRule, Guid studentId)
        {
            await using var transaction = await _sql.Database.BeginTransactionAsync();
            try
            {
                if (lockRule.Name.IsNullOrEmpty())
                    throw new UnprocessableEntityException();

                if (!_sql.Students.Any(x => x.Id == studentId))
                    throw new ReferredEntityNotFoundException();

                if (lockRule.StartTime > lockRule.EndTime)
                    throw new UnprocessableEntityException()
                        { Message = "A kezdési idő nem lehet nagyobb mint a befejezési idő!" };

                if (lockRule.DayOfWeek < 1 || lockRule.DayOfWeek > 5)
                    throw new UnprocessableEntityException()
                        { Message = "A hét napjának számának 1 és 5 között kell lennie!" };

                if (_sql.LockRules.Any(x =>
                        x.DayOfWeek == lockRule.DayOfWeek && x.StartTime == lockRule.StartTime &&
                        x.EndTime == lockRule.EndTime))
                {
                    var existingRule = _sql.LockRules.Single(x =>
                        x.DayOfWeek == lockRule.DayOfWeek && x.StartTime == lockRule.StartTime &&
                        x.EndTime == lockRule.EndTime);

                    var newRole = new StudentLockRule()
                    {
                        LockRuleId = existingRule.LockRuleId,
                        StudentId = studentId
                    };

                    _sql.StudentsLockRules.Add(newRole);
                    await _sql.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return;
                }

                _sql.LockRules.Add(lockRule);
                await _sql.SaveChangesAsync();

                var ruleId = _sql.LockRules.Single(x =>
                        x.DayOfWeek == lockRule.DayOfWeek && x.StartTime == lockRule.StartTime &&
                        x.EndTime == lockRule.EndTime)
                    .LockRuleId;

                _sql.StudentsLockRules.Add(new StudentLockRule()
                {
                    LockRuleId = ruleId,
                    StudentId = studentId
                });

                await _sql.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task CreateManyRestrictions(LockRule[] lockRules, Guid studentId)
        {
            await using var transaction = await _sql.Database.BeginTransactionAsync();
            try
            {
                foreach (var rule in lockRules)
                {
                    if (rule.Name.IsNullOrEmpty())
                        throw new UnprocessableEntityException();

                    if (!_sql.Students.Any(x => x.Id == studentId))
                        throw new ReferredEntityNotFoundException();

                    if (rule.StartTime > rule.EndTime)
                        throw new UnprocessableEntityException()
                            { Message = "A kezdési idő nem lehet nagyobb mint a befejezési idő!" };

                    if (rule.DayOfWeek < 1 || rule.DayOfWeek > 5)
                        throw new UnprocessableEntityException()
                            { Message = "A hét napjának számának 1 és 5 között kell lennie!" };

                    if (_sql.LockRules.Any(x =>
                        x.DayOfWeek == rule.DayOfWeek && x.StartTime == rule.StartTime &&
                        x.EndTime == rule.EndTime))
                    {
                        var existingRule = _sql.LockRules.Single(x =>
                            x.DayOfWeek == rule.DayOfWeek && x.StartTime == rule.StartTime &&
                            x.EndTime == rule.EndTime);

                        var newRole = new StudentLockRule()
                        {
                            LockRuleId = existingRule.LockRuleId,
                            StudentId = studentId
                        };

                        _sql.StudentsLockRules.Add(newRole);
                        await _sql.SaveChangesAsync();
                        continue;
                    }

                    _sql.LockRules.Add(rule);
                    await _sql.SaveChangesAsync();

                    var ruleId = _sql.LockRules.Single(x =>
                            x.DayOfWeek == rule.DayOfWeek && x.StartTime == rule.StartTime &&
                            x.EndTime == rule.EndTime)
                        .LockRuleId;

                    _sql.StudentsLockRules.Add(new StudentLockRule()
                    {
                        LockRuleId = ruleId,
                        StudentId = studentId
                    });

                    await _sql.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch
            {
               await transaction.RollbackAsync();
                throw;
            }
        }
    }
}