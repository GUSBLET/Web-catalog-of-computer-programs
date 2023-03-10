using UI.MicroServises;
using UI.Models;

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
        //_catalogServise.GetModelByName("test");
        return View();
    }

    [HttpGet]
    public IActionResult AddNewItem()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddNewItem(AddNewItem model)
    {
        if(ModelState.IsValid)
        {
            var response=  await _catalogServise.AddNewRecord(ModelConvertation.ModelConvertationToSendIntoBusinessLogicBusnesLyar(model));
            if(response == System.Net.HttpStatusCode.OK)
                return RedirectToAction("Index");

            return RedirectToAction("Error");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Error()
    {
        return View();
    }
}

