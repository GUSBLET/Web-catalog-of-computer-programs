namespace DataAccessLayer.Repositories;

public class TypeRepository : IBaseRepository<Entities.Type>
{
    private readonly ApplicationDbContext _dataBase;

    public TypeRepository(ApplicationDbContext dataBase) => _dataBase = dataBase;

    public async Task<bool> Add(Entities.Type entity)
    {
        try
        {
            await _dataBase.Types.AddAsync(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public async Task<bool> Delete(Entities.Type entity)
    {
        try
        {
            _dataBase.Types.Remove(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public async Task<Entities.Type> Update(Entities.Type entity)
    {
        _dataBase.Types.Update(entity);
        await _dataBase.SaveChangesAsync();

        return entity;
    }

    IQueryable<Entities.Type> IBaseRepository<Entities.Type>.Select()
    {
        return _dataBase.Types;
    }
}
