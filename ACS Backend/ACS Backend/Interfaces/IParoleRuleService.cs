namespace ACS_Backend.Interfaces;

public interface IParoleRuleService
{
    public ParoleRule GetParoleRuleById(int id);
    public Array GetParoleRules();
    public Task CreateParoleRule(ParoleRule paroleRule);
    public Task DeleteParoleRule(int id);
}