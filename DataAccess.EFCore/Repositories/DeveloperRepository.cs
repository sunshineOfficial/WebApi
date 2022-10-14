using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.Repositories;

public class DeveloperRepository : GenericRepository<Developer>, IDeveloperRepository
{
    public DeveloperRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Developer>> GetPopularDevelopers(int count)
    {
        return await _context.Developers.OrderByDescending(d => d.Followers).Take(count).ToListAsync();
    }
}