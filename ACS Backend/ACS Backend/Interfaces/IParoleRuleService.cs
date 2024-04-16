using ACS_Backend.Model;

namespace ACS_Backend.Interfaces;

public interface IParoleRuleService
{
    public ParoleRule GetParoleRuleById(int id);
    public Array GetParoleRules();
    public Task CreateParoleRule(NewNoteModel paroleRule);
    public Task DeleteParoleRule(int id);
}