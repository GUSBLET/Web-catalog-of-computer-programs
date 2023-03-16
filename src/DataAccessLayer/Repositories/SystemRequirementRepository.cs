namespace DataAccessLayer.Repositories;

public class SystemRequirementRepository : IBaseRepository<SystemRequirement>
{
    private readonly ApplicationDbContext _dataBase;

    public SystemRequirementRepository(ApplicationDbContext dataBase) => _dataBase = dataBase;

    public async Task<bool> Add(SystemRequirement entity)
    {
        try
        {
            await _dataBase.SystemRequirements.AddAsync(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public async Task<bool> Delete(SystemRequirement entity)
    {
        try
        {
            _dataBase.SystemRequirements.Remove(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public IQueryable<SystemRequirement> Select()
    {
        return _dataBase.SystemRequirements;
    }

    public async Task<SystemRequirement> Update(SystemRequirement entity)
    {
        _dataBase.SystemRequirements.Update(entity);
        await _dataBase.SaveChangesAsync();

        return entity;
    }
}
