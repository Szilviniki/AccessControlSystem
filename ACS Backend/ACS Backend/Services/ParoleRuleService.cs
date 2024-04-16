using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;

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

    public async Task CreateParoleRule(NewNoteModel paroleRule)
    {
        if (_sql.ParoleRules.Any(rule => rule.Name == paroleRule.Name))
            throw new ItemAlreadyExistsException();
        var note = new ParoleRule()
        {
            Name = paroleRule.Name,
            DayOfWeek = paroleRule.DayOfWeek,
            StartTime = null,
            EndTime = null
        };
        
        _sql.ParoleRules.Add(note);
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