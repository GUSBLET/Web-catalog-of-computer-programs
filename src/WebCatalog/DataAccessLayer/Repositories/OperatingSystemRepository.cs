namespace DataAccessLayer.Repositories;

public class OperatingSystemRepository : IBaseRepository<Domain.Entities.OperatingSystem>
{
    private readonly ApplicationDbContext _dataBase;

    public OperatingSystemRepository(ApplicationDbContext dataBase) => _dataBase = dataBase;

    public async Task<bool> Add(Domain.Entities.OperatingSystem entity)
    {
        try
        {
            await _dataBase.OperatingSystems.AddAsync(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public async Task<bool> Delete(Domain.Entities.OperatingSystem entity)
    {
        try
        {
            _dataBase.OperatingSystems.Remove(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public IQueryable<Domain.Entities.OperatingSystem> Select()
    {
        return _dataBase.OperatingSystems;
    }

    public async Task<Domain.Entities.OperatingSystem> Update(Domain.Entities.OperatingSystem entity)
    {
        _dataBase.OperatingSystems.Update(entity);
        await _dataBase.SaveChangesAsync();

        return entity;
    }
}
