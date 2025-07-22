namespace Strogue.Aira.Persistence.Models;

public class ApplicationUser : IdentityUser
{
    public int TenantId { get; set; } = 1;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public byte[] ProfilePicture { get; set; } = [];
    public int PositionId { get; set; } = 1;

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}