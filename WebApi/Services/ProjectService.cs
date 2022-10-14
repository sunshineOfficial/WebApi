using DataAccess.EFCore;
using Domain.Entities;
using Domain.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services;

public class ProjectService : IGenericRepository<Project>
{
    private readonly ApplicationContext _context;

    public ProjectService(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<Project> GetById(int id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<IEnumerable<Project>> GetAll()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<IEnumerable<Project>> Find(Expression<Func<Project, bool>> expression)
    {
        return await _context.Projects.Where(expression).ToListAsync();
    }

    public async Task<int> Add(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project.Id;
    }

    public async Task<IEnumerable<int>> AddRange(IEnumerable<Project> projects)
    {
        _context.Projects.AddRange(projects);
        await _context.SaveChangesAsync();
        return projects.Select(p => p.Id);
    }

    public async Task<Project> Update(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
        return project;
    }
    
    public async Task<IEnumerable<Project>> UpdateRange(IEnumerable<Project> projects)
    {
        _context.Projects.UpdateRange(projects);
        await _context.SaveChangesAsync();
        return projects;
    }

    public async Task<Project> Remove(Project project)
    {
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<IEnumerable<Project>> RemoveRange(IEnumerable<Project> projects)
    {
        _context.Projects.RemoveRange(projects);
        await _context.SaveChangesAsync();
        return projects;
    }
}