namespace ACS_Backend.Interfaces;

public interface IParoleRuleService
{
    public Privilege GetParoleRuleById(int id);
    public Array GetParoleRules();
    public Task CreateParoleRule(Privilege paroleRule);
    public Task DeleteParoleRule(int id);
}