using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movian.Business.Interfaces;
using Movian.Business.Models;
using Movian.Data.Contexts;

namespace Movian.Data.Repositories
{
  public abstract class Repository<TEntity> : IRepository<TEntity>
      where TEntity : Entity, new()
  {
    protected readonly AppDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(AppDbContext db)
    {
      Db = db;
      DbSet = db.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
      DbSet.Add(entity);
      await SaveChangesAsync();
    }

    public virtual async Task<IList<TEntity>> GetAllAsync()
    {
      return await DbSet.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
      return await DbSet.FindAsync(id);
    }

    public virtual async Task RemoveAsync(Guid id)
    {
      DbSet.Remove(new TEntity { Id = id });
      await SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
      return await Db.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
      DbSet.Update(entity);
      await SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
    {
      return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public void Dispose()
    {
      Db?.Dispose();
    }
  }
}