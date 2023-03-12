

namespace UI.Controllers;

public class HomeController : Controller
{
    private readonly ICatalogServise _catalogServise;
    private ICollection<ViewItemsViewModel> _listOfItems;
    

    public HomeController(ICatalogServise catalogServise)
    {
        _catalogServise = catalogServise;
    }

    public IActionResult Index()
    {
        _listOfItems = _catalogServise.Select().Result;
        return View(_listOfItems);
    }

    [HttpGet]
    public IActionResult ViewItem(int id)
    {
        var response = _catalogServise.SelectItemById(id).Result;
        if(response is null)
            return RedirectToAction("Error");
        return View(response);
    }   

    [HttpPost]
    public async Task<IActionResult> UpdateItem(ViewUpdateOneItemViewModel model)
    { 
        var response = await _catalogServise.UpdateItem(model);
        if(response == System.Net.HttpStatusCode.OK)
            return RedirectToAction("Index");

        return RedirectToAction("Error");
    }

    [HttpGet]
    public IActionResult AddNewItem()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddNewItem(AddNewItemViewModel model)
    {
        if(ModelState.IsValid)
        {
            var response =  await _catalogServise.AddNewRecord(ModelConvertation.ModelConvertationToSendIntoBusinessLogicLyar(model));
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

