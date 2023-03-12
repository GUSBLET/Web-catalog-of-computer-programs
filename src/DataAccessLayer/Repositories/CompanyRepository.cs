namespace DataAccessLayer.Repositories;

public class CompanyRepository : IBaseRepository<Company>
{
    private readonly ApplicationDbContext _dataBase;

    public CompanyRepository(ApplicationDbContext dataBase) => _dataBase = dataBase;

    public async Task<bool> Add(Company entity)
    {
        try
        {
            await _dataBase.Companies.AddAsync(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public async Task<bool> Delete(Company entity)
    {
        try
        {
            _dataBase.Companies.Remove(entity);
            await _dataBase.SaveChangesAsync();
        }
        catch { return await Task.FromResult(false); }

        return await Task.FromResult(true);
    }

    public IQueryable<Company> Select()
    {
        return _dataBase.Companies;
    }

    public async Task<Company> Update(Company entity)
    {
        _dataBase.Companies.Update(entity);
        await _dataBase.SaveChangesAsync();

        return entity;
    }

    //public async Task<List<>> SelectAll()
    //{
    //    var response = await _dataBase.Programs
    //        .FromSql($"SELECT * FROM dbo.Programs ")
    //        .ToListAsync();

    //    return response;
    //}
}
