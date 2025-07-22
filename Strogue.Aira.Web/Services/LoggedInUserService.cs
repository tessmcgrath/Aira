namespace Strogue.Aira.Web.Services;

public class LoggedInUserService(IHttpContextAccessor httpContextAccessor) : ILoggedInUserService
{
    public string UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
    public int TenantId => Convert.ToInt32(httpContextAccessor.HttpContext?.User.GetCurrentUserProperty(CustomClaimType.TenantId)!);
}