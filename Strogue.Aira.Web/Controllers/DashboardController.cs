namespace Strogue.Aira.Web.Controllers;

public class DashboardController : BaseController<DashboardController>
{
    public IActionResult Index()
    {
        return View();
    }
}