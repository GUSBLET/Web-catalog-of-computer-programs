namespace BusinessLogic.Interfaces;

public interface ICatalogServise
{
    Task<HttpStatusCode> AddNewRecord(ModelOfItemDTO model);
    Task<List<ViewItemsViewModel>> Select();
    Task<ViewOneItemViewModel> SelectItemById(int id);
}
