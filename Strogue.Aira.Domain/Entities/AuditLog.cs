namespace Strogue.Aira.Domain.Entities;

public class AuditLog
{
    public int AuditLogId { get; set; }
    public required string UserId { get; set; } = string.Empty;
    public required string EntityName { get; set; } = string.Empty;
    public required string Action { get; set; } = string.Empty;
    public required DateTime Timestamp { get; set; }
    public required string Changes { get; set; } = string.Empty;
    public int TenantId { get; set; }
}