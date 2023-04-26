using System;
namespace ToDoApp.DAL.Interfaces
{
   public interface IUnitOfWork : IDisposable
    {
      IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

      int SaveChanges();

      Task<int> SaveChangesAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork
    {
    }
}

