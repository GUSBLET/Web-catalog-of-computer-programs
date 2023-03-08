namespace UI;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {   
        services.AddScoped<IBaseRepository<Company>, CompanyRepository>();
        services.AddScoped<IBaseRepository<SystemRequirement>, SystemRequirementRepository>();
        services.AddScoped<IBaseRepository<DataAccessLayer.Entities.Type>, TypeRepository>();
        services.AddScoped<IBaseRepository<DataAccessLayer.Entities.Program>, ProgramRepository>();
        services.AddScoped<IBaseRepository<DataAccessLayer.Entities.OperatingSystem>, OperatingSystemRepository>();
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<ICatalogServise ,CatalogServise>();
    }
}
