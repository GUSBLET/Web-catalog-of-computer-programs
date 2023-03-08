namespace UI.Controllers;

public class HomeController : Controller
{
    private readonly ICatalogServise _catalogServise;

    public HomeController(ICatalogServise catalogServise)
    {
        _catalogServise = catalogServise;
    }

    public IActionResult Index()
    {
        _catalogServise.GetModelByName("test");
        return View();
    }
}
