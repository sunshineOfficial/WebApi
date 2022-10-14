namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IDeveloperRepository Developers { get; }
    IProjectRepository Projects { get; }
    Task<int> Complete();
}