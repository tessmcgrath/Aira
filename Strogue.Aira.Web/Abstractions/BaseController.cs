namespace Strogue.Aira.Web.Abstractions;

[Authorize]
public abstract class BaseController<T> : Controller
{
    private SharedResourceService _sharedLocalizer = null!;
    protected SharedResourceService Resources =>
        _sharedLocalizer ??= HttpContext.RequestServices.GetService<SharedResourceService>()!;

    private IMediator _mediator = null!;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}