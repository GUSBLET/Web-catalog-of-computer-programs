namespace UI.MicroServises;

public class ModelConvertation
{
    public static ModelOfItemDTO ModelConvertationToSendIntoBusinessLogicLyar(AddNewItemViewModel model)
    {
        return new ModelOfItemDTO
        {
            Name = model.Name,
            Description = model.Description,
            Company = model.CompanyName,
            DescriptionOfCompany = model.CompanyDescription,
            License = model.License,
            MultiUserMode = model.MultiUserMode,
            CrossPlatform = model.CrossPlatform,
            UsingConnection = model.UsingConnection,
            ReleaseDate = model.ReleaseDate,
            Logo = model.Logo,
            OperatingSystems = model.OperatingSystems,
            Requirements = model.Requirements,
            Type = model.ProgramType,
            Version = model.Version,
            Weight = model.Weight
        };
    }      

}
