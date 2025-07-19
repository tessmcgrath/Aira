namespace Strogue.Aira.Domain.Entities;

public class Country : AuditableEntity
{
    public int CountryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string TimeZone { get; set; } = string.Empty;
    public string GmtOffset { get; set; } = string.Empty;

    public int ContinentId { get; set; }
    public required Continent Continent { get; set; }
}