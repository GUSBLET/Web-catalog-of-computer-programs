namespace BusinessLogic.Interfaces;

public interface ICatalogServise
{
    Task<HttpStatusCode> AddNewRecord(ModelOfItemDTO model);
    Task<HttpStatusCode> UpdateItem(ViewUpdateOneItemViewModel model);
    Task<List<ViewItemsViewModel>> Select();
    Task<ViewUpdateOneItemViewModel> SelectItemById(int id);
}
