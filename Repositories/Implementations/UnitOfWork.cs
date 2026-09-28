namespace LAB5_Fatima.Repositories.Implementations;

using System.Collections;
using LAB5_Fatima.Models;
using LAB5_Fatima.Repositories.Implementations;
using LAB5_Fatima.Repositories.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable? _repositories;
    private readonly Lab05DbContext _context;

    public UnitOfWork(Lab05DbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public Task<int> Complete()
    {
        return _context.SaveChangesAsync();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (_repositories!.ContainsKey(type))
        {
            return (IGenericRepository<TEntity>)_repositories[type]!;
        }

        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

        if (repositoryInstance != null)
        {
            _repositories.Add(type, repositoryInstance);
            return (IGenericRepository<TEntity>)repositoryInstance;
        }

        throw new Exception($"Could not create repository instance for type {type}");
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
