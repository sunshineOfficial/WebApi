using Domain.Entities;

namespace Domain.Interfaces;

public interface IDeveloperRepository : IGenericRepository<Developer>
{
    Task<IEnumerable<Developer>> GetPopularDevelopers(int count);
}