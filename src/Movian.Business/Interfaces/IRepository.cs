using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Movian.Business.Models;

namespace Movian.Business.Interfaces
{
  public interface IRepository<TEntity> : IDisposable where TEntity : Entity
  {
    Task AddAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(Guid id);
    Task<IList<TEntity>> GetAllAsync();
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(Guid id);
    Task<int> SaveChangesAsync();
    Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
  }
}