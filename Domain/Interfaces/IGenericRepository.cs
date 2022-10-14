using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
    Task<int> Add(T entity);
    Task<IEnumerable<int>> AddRange(IEnumerable<T> entities);
    Task<T> Update(T entity);
    Task<IEnumerable<T>> UpdateRange(IEnumerable<T> entities);
    Task<T> Remove(T entity);
    Task<IEnumerable<T>> RemoveRange(IEnumerable<T> entities);
}