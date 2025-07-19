namespace Strogue.Aira.Web.Services;

public class SharedResourceService(IStringLocalizer<SharedResource> localizer)
{
    private readonly IStringLocalizer<SharedResource> _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));

    public LocalizedString this[string key] => _localizer[key];

    public LocalizedString GetLocalizedString(string key) => _localizer[key];
}