using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class SqlRegionRepository:IRegionRepository
{
    private readonly NZWalksDbContext _dbContext;
    public SqlRegionRepository(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Region>> GetAllAsync()
    {
        return await _dbContext.Regions.ToListAsync();
    }
}