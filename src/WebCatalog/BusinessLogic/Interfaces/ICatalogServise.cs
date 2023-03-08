namespace BusinessLogic.Interfaces;

public interface ICatalogServise
{
    Task<HttpStatusCode> AddNewRecord(AddingModel model);
    Task<ModelSelection> GetModelByName(string name);
}
