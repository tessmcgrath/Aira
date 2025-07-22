namespace Strogue.Aira.Application.Contracts;

public interface ILoggedInUserService
{
    public string UserId { get; }
    public int TenantId { get; }
}