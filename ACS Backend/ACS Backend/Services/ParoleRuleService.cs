using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;

namespace ACS_Backend.Services;

public class ParoleRuleService : IParoleRuleService
{
    private SQL _sql;

    public ParoleRuleService(SQL sql)
    {
        _sql = sql;
    }

    public ParoleRule GetParoleRuleById(int id)
    {
        if (_sql.ParoleRules.Any(x => x.Id == id)) throw new ItemNotFoundException();
        return _sql.ParoleRules.First(x => x.Id == id);
    }

    public Array GetParoleRules()
    {
        return _sql.ParoleRules.ToArray();
    }

    public async Task CreateParoleRule(ParoleRule paroleRule)
    {
        if (_sql.LockRules.Any(restriction1 => restriction1.Name == paroleRule.Name))
            throw new ItemAlreadyExistsException();
        _sql.ParoleRules.Add(paroleRule);
        await _sql.SaveChangesAsync();
    }

    public Task DeleteParoleRule(int id)
    {
        if (_sql.ParoleRules.Any(x => x.Id == id))
        {
            _sql.Remove(_sql.ParoleRules.First(x => x.Id == id));
            return _sql.SaveChangesAsync();
        }
        else
        {
            throw new ItemNotFoundException();
        }
    }
}