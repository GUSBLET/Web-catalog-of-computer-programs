namespace DataAccessLayer.Repositories;

public class ProgramRepository : IBaseRepository<Program>
{
    private readonly ApplicationDbContext _dataBase;

    public ProgramRepository(ApplicationDbContext dataBase) => _dataBase = dataBase;

    public async Task<bool> Add(Program entity)
    {
        try
        {
            await _dataBase.Programs.AddAsync(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public async Task<bool> Delete(Program entity)
    {
        try
        {
            _dataBase.Programs.Remove(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public IQueryable<Program> Select()
    {
        return _dataBase.Programs;
    }

    public async Task<Program> Update(Program entity)
    {
        _dataBase.Programs.Update(entity);
        await _dataBase.SaveChangesAsync();

        return entity;
    }
}
