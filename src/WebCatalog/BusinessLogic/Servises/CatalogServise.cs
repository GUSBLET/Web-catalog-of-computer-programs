namespace BusinessLogic.Servises;

public class CatalogServise : ICatalogServise
{
    private readonly IBaseRepository<Domain.Entities.Type> _typeRepository;
    private readonly IBaseRepository<Domain.Entities.OperatingSystem> _operatingSystemRepository;
    private readonly IBaseRepository<Program> _programRepository;
    private readonly IBaseRepository<SystemRequirement> _systemRequirementRepository;
    private readonly IBaseRepository<Company> _companyRepository;

    public CatalogServise(
        IBaseRepository<Company> companyRepository,
        IBaseRepository<Domain.Entities.Type> typeRepository,
        IBaseRepository<Program> programRepository,
        IBaseRepository<SystemRequirement> systemRequirementRepository,
        IBaseRepository<Domain.Entities.OperatingSystem> operatingSystemRepository)
    {
        _companyRepository = companyRepository;
        _typeRepository = typeRepository;
        _programRepository = programRepository;
        _systemRequirementRepository = systemRequirementRepository;
        _operatingSystemRepository = operatingSystemRepository;
    }

    public async Task<HttpStatusCode> AddNewRecord(ModelOfItemDTO model)
    {
        try
        {
            // Checking the existence of the program.
            if (_programRepository.Select().Where(x => x.Name == model.Name).FirstOrDefaultAsync().Result != null)
                return HttpStatusCode.BadRequest;

            // Checking the existence of the company and if company does not exist then create a new company.
            var responseCompany = await _companyRepository.Select().Where(x => x.Name == model.Company).FirstOrDefaultAsync();
            if(responseCompany == null)
            {
                await _companyRepository.Add(ModelCompanyPreparationForPsuh(ref model));
                responseCompany = await _companyRepository.Select().Where(x => x.Name == model.Company).FirstOrDefaultAsync();
            }

            // Checking the existence of the program type and if company does not exist then create a new type.
            var responseType = await _typeRepository.Select().Where(x => x.Name == model.Type).FirstOrDefaultAsync();
            if (responseType == null)
            {
                await _typeRepository.Add(ModelTypePreparationForPsuh(ref model));
                responseType = await _typeRepository.Select().Where(x => x.Name == LineToLowRegister(model.Type)).FirstOrDefaultAsync();
            }

            // Checking the existence of the system requirements and if company does not exist then create a new.
            var responseRequirementList = new List<SystemRequirement>();
            foreach (var item in SplitLine(model.Requirements))
            {
                var responseRequirement = await _systemRequirementRepository.Select().Where(x => x.Name == LineToLowRegister(item)).FirstOrDefaultAsync();
                if (responseRequirement == null)
                {
                    await _systemRequirementRepository.Add(ModelRequirementPreparationForPsuh(item));
                    responseRequirement = await _systemRequirementRepository.Select().Where(x => x.Name == LineToLowRegister(item)).FirstOrDefaultAsync();
                    responseRequirementList.Add(responseRequirement);
                }
                else
                    responseRequirementList.Add(responseRequirement);
            }

            // Checking the existence of the operating systems and if company does not exist then create a new.
            var responseOperatingSystemList = new List<Domain.Entities.OperatingSystem>();
            foreach (var item in SplitLine(model.OperatingSystems))
            {
                var responseOperatingSystem = await _operatingSystemRepository.Select().Where(x => x.Name == LineToLowRegister(item)).FirstOrDefaultAsync();
                if (responseOperatingSystem == null)
                {
                    await _operatingSystemRepository.Add(ModelOperatingSystemPreparationForPsuh(item));
                    responseOperatingSystem = await _operatingSystemRepository.Select().Where(x => x.Name == LineToLowRegister(item)).FirstOrDefaultAsync();
                    responseOperatingSystemList.Add(responseOperatingSystem);
                }
                else
                    responseOperatingSystemList.Add(responseOperatingSystem);
            }

            // Adding program model into main table.
            var newPrpgram = ModelProgramPreparationForPsuh(ref model, responseCompany, responseType, 
                                                 responseRequirementList, responseOperatingSystemList);
            await _programRepository.Add(newPrpgram);

            return HttpStatusCode.OK;
        }
        catch (Exception)
        {
            return HttpStatusCode.InternalServerError;
        }
    }

    public async Task<List<ViewItemsViewModel>> Select()
    {
        try
        {
            var result =
                from programs in _programRepository.Select()
                join companies in _companyRepository.Select() on programs.Company.Id equals companies.Id into bufferCompany
                from subCompanies in bufferCompany.DefaultIfEmpty()
                join types in _typeRepository.Select() on programs.Type.Id equals types.Id into bufferType
                from subTypes in bufferType.DefaultIfEmpty()
                select new ViewItemsViewModel
                {
                    Id = programs.Id,
                    ProgramName = programs.Name,
                    Version = programs.Version,
                    ReleaseDate = programs.ReleaseDate,
                    Type = subTypes.Name,
                    CompanyName = subCompanies.Name,
                    Logo = programs.Logo,
                };

            return result.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ViewOneItemViewModel> SelectItemById(int id)
    {
        try
        {
            // Select requirements and operating systems of item.
            var r = _programRepository.Select().Include(r => r.Requirements).Include(x => x.OperatingSystems).Where(x => x.Id == id).ToList();
            
            // Select item and fully fill.
            var result =
                from program in _programRepository.Select()
                where program.Id == id
                join company in _companyRepository.Select() on program.Company.Id equals company.Id into bufferCompany
                from subCompany in bufferCompany.DefaultIfEmpty()
                join types in _typeRepository.Select() on program.Type.Id equals types.Id into bufferType
                from subTypes in bufferType.DefaultIfEmpty()

                select new ViewOneItemViewModel
                {
                    Id = id,
                    Name = program.Name,
                    Description = program.Description,
                    CrossPlatform = program.CrossPlatform,
                    MultiUserMode = program.MultiUserMode,
                    License = program.License,
                    Version = program.Version,
                    UsingConnection = program.UsingConnection,
                    ReleaseDate = program.ReleaseDate,
                    CompanyName = subCompany.Name,
                    CompanyDescription = subCompany.Description,
                    ProgramType = subTypes.Name,
                    Logo = program.Logo       ,
                    Weight = program.Weight,
                    Requirements = MergeLine(r.First().Requirements.Select(x => x.Name).ToList()),
                    OperatingSystems = MergeLine(r.First().OperatingSystems.Select(x => x.Name).ToList())
                };

            return result.First();
        }
        catch (Exception)
        {
            return null;
        }
    }

    private string[] SplitLine(string line)
    {
        return line.Split('/');
    }

    private string LineToLowRegister(string line)
    {
        return line.ToLower();
    }

    private Program ModelProgramPreparationForPsuh(ref ModelOfItemDTO model, Company company, Domain.Entities.Type type,
                     List<SystemRequirement> systemRequirements, List<Domain.Entities.OperatingSystem> operatingSystems)
    {
        return new Program()
        {
            Name = model.Name,
            Description = model.Description,
            ReleaseDate = model.ReleaseDate,
            CrossPlatform = model.CrossPlatform,
            License = model.License,
            UsingConnection = model.UsingConnection,
            Version = model.Version,
            MultiUserMode = model.MultiUserMode,
            Logo = PackingPhoto(model.Logo).Data,
            Company = company,
            Type = type,
            OperatingSystems = operatingSystems,
            Requirements = systemRequirements,
            Weight = model.Weight
        };
    }

    private Company ModelCompanyPreparationForPsuh(ref ModelOfItemDTO model)
    {
        return new Company()
        {
            Name = model.Company,
            Description = model.DescriptionOfCompany

        };
    }

    private Domain.Entities.OperatingSystem ModelOperatingSystemPreparationForPsuh(string model)
    {
        return new Domain.Entities.OperatingSystem()
        {
            Name = LineToLowRegister(model)
        };
    }

    private Domain.Entities.Type ModelTypePreparationForPsuh(ref ModelOfItemDTO model)
    {
        return new Domain.Entities.Type()
        {
            Name = LineToLowRegister(model.Type)
        };
    }

    private SystemRequirement ModelRequirementPreparationForPsuh(string model)
    {
        return new SystemRequirement()
        {
            Name = LineToLowRegister(model)
        };
    }

    private PhotoData PackingPhoto(IFormFile file)
    {
        using (var binaryReader = new BinaryReader(file.OpenReadStream())) 
        {
            var photo = new PhotoData(binaryReader.ReadBytes((int)file.Length));
            return photo;
        }
    }

    private string MergeLine(ICollection<string> lines)
    {
        string response = string.Empty;

        foreach (var line in lines) 
        { 
            response += line + "/";
        }
        
        return response.Remove(response.Length - 1);
    }

}
