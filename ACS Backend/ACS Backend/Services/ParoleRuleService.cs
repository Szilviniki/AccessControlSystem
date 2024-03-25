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

    public Privilege GetParoleRuleById(int id)
    {
        if (_sql.Privileges.Any(x => x.Id == id)) throw new ItemNotFoundException();
        return _sql.Privileges.First(x => x.Id == id);
    }

    public Array GetParoleRules()
    {
        return _sql.Privileges.ToArray();
    }

    public async Task CreateParoleRule(Privilege paroleRule)
    {
        if (_sql.Restrictions.Any(restriction1 => restriction1.Name == paroleRule.Name))
            throw new ItemAlreadyExistsException();
        _sql.Privileges.Add(paroleRule);
        await _sql.SaveChangesAsync();
    }

    public Task DeleteParoleRule(int id)
    {
        if (_sql.Privileges.Any(x => x.Id == id))
        {
            _sql.Remove(_sql.Privileges.First(x => x.Id == id));
            return _sql.SaveChangesAsync();
        }
        else
        {
            throw new ItemNotFoundException();
        }
    }
}