using DataAccess.EFCore;
using Domain.Entities;
using Domain.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services;

public class DeveloperService : IGenericRepository<Developer>
{
    private readonly ApplicationContext _context;

    public DeveloperService(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<Developer> GetById(int id)
    {
        return await _context.Developers.FindAsync(id);
    }

    public async Task<IEnumerable<Developer>> GetAll()
    {
        return await _context.Developers.ToListAsync();
    }

    public async Task<IEnumerable<Developer>> Find(Expression<Func<Developer, bool>> expression)
    {
        return await _context.Developers.Where(expression).ToListAsync();
    }

    public async Task<int> Add(Developer developer)
    {
        _context.Developers.Add(developer);
        await _context.SaveChangesAsync();
        return developer.Id;
    }

    public async Task<IEnumerable<int>> AddRange(IEnumerable<Developer> developers)
    {
        _context.Developers.AddRange(developers);
        await _context.SaveChangesAsync();
        return developers.Select(d => d.Id);
    }

    public async Task<Developer> Update(Developer developer)
    {
        _context.Developers.Update(developer);
        await _context.SaveChangesAsync();
        return developer;
    }
    
    public async Task<IEnumerable<Developer>> UpdateRange(IEnumerable<Developer> developers)
    {
        _context.Developers.UpdateRange(developers);
        await _context.SaveChangesAsync();
        return developers;
    }

    public async Task<Developer> Remove(Developer developer)
    {
        _context.Developers.Remove(developer);
        await _context.SaveChangesAsync();
        return developer;
    }

    public async Task<IEnumerable<Developer>> RemoveRange(IEnumerable<Developer> developers)
    {
        _context.Developers.RemoveRange(developers);
        await _context.SaveChangesAsync();
        return developers;
    }
    
    public async Task<IEnumerable<Developer>> GetPopularDevelopers(int count)
    {
        return await _context.Developers.OrderByDescending(d => d.Followers).Take(count).ToListAsync();
    }
}