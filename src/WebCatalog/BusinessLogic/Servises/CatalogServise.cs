using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Servises;

public class CatalogServise : ICatalogServise
{
    private readonly IBaseRepository<DataAccessLayer.Entities.Type> _typeRepository;
    private readonly IBaseRepository<DataAccessLayer.Entities.OperatingSystem> _operatingSystemRepository;
    private readonly IBaseRepository<Program> _programRepository;
    private readonly IBaseRepository<SystemRequirement> _systemRequirementRepository;
    private readonly IBaseRepository<Company> _companyRepository;

    public CatalogServise(
        IBaseRepository<Company> companyRepository,
        IBaseRepository<DataAccessLayer.Entities.Type> typeRepository,
        IBaseRepository<Program> programRepository,
        IBaseRepository<SystemRequirement> systemRequirementRepository,
        IBaseRepository<DataAccessLayer.Entities.OperatingSystem> operatingSystemRepository)
    {
        _companyRepository = companyRepository;
        _typeRepository = typeRepository;
        _programRepository = programRepository;
        _systemRequirementRepository = systemRequirementRepository;
        _operatingSystemRepository = operatingSystemRepository;
    }

    public Task<HttpStatusCode> AddNewRecord(AddingModel model)
    {
        _companyRepository.Select();
        throw new NotImplementedException();
    }

    public Task<ModelSelection> GetModelByName(string name)
    {
        _companyRepository.Select();
        throw new NotImplementedException();
    }
}
