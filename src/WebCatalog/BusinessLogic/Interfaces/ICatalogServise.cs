namespace BusinessLogic.Interfaces;

public interface ICatalogServise
{
    Task<HttpStatusCode> AddNewRecord(ModelOfItemBL model);
    Task<List<ModelOfItemBL>> Select();
}
