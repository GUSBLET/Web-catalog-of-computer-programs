using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

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

    public async Task<HttpStatusCode> AddNewRecord(ModelOfItemBL model)
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
            var responseOperatingSystemList = new List<DataAccessLayer.Entities.OperatingSystem>();
            foreach (var item in SplitLine(model.Requirements))
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

    public Task<List<ModelOfItemBL>> Select()
    {
        try
        {
            //var response = new List<ModelSelection>();



            //for (int i = 0; i < _companyRepository.; i++)
            //{

            //}

            //return response;
            throw new NotImplementedException();
        }
        catch (Exception)
        {

            throw;
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

    private Program ModelProgramPreparationForPsuh(ref ModelOfItemBL model, Company company, DataAccessLayer.Entities.Type type,
                     List<SystemRequirement> systemRequirements, List<DataAccessLayer.Entities.OperatingSystem> operatingSystems)
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
            Logo = PackingPhotos(model.Logo).Data,
            Company = company,
            Type = type,
            OperatingSystems = operatingSystems,
            Requirements = systemRequirements
        };
    }

    private Company ModelCompanyPreparationForPsuh(ref ModelOfItemBL model)
    {
        return new Company()
        {
            Name = model.Company,
            Description = model.DescriptionOfCompany

        };
    }

    private DataAccessLayer.Entities.OperatingSystem ModelOperatingSystemPreparationForPsuh(string model)
    {
        return new DataAccessLayer.Entities.OperatingSystem()
        {
            Name = LineToLowRegister(model)
        };
    }

    private DataAccessLayer.Entities.Type ModelTypePreparationForPsuh(ref ModelOfItemBL model)
    {
        return new DataAccessLayer.Entities.Type()
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

    private Task<ModelOfItemBL> GetModelByIndex(int id)
    {
        throw new NotImplementedException();
    }

    private Task<ModelOfItemBL> GetModelByName(string name)
    {
        _companyRepository.Select();
        throw new NotImplementedException();
    }

    private PhotoData PackingPhotos(IFormFile file)
    {
        using (var binaryReader = new BinaryReader(file.OpenReadStream())) 
        {
            var photo = new PhotoData(binaryReader.ReadBytes((int)file.Length));
            return photo;
        }
    }

}
