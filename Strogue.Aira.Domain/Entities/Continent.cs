namespace Strogue.Aira.Domain.Entities;

public class Continent : AuditableEntity
{
    public int ContinentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public IList<Country> Countries { get; set; } = new List<Country>();
}