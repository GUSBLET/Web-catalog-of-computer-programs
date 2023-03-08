namespace DataAccessLayer.Repositories;

public class OperatingSystemRepository : IBaseRepository<Entities.OperatingSystem>
{
    private readonly ApplicationDbContext _dataBase;

    public OperatingSystemRepository(ApplicationDbContext dataBase) => _dataBase = dataBase;

    public async Task<bool> Add(Entities.OperatingSystem entity)
    {
        try
        {
            await _dataBase.OperatingSystems.AddAsync(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public async Task<bool> Delete(Entities.OperatingSystem entity)
    {
        try
        {
            _dataBase.OperatingSystems.Remove(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public IQueryable<Entities.OperatingSystem> Select()
    {
        return _dataBase.OperatingSystems;
    }

    public async Task<Entities.OperatingSystem> Update(Entities.OperatingSystem entity)
    {
        _dataBase.OperatingSystems.Update(entity);
        await _dataBase.SaveChangesAsync();

        return entity;
    }
}
