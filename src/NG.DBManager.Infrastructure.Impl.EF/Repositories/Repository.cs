using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using NG.DBManager.Infrastructure.Impl.EF.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null) { return; }

            DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (!entities.Any()) { return; }

            DbSet.AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual TEntity Get(object id)
        {
            return DbSet.Find(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, object>>[] includes)
        {
            if (includes == null)
                return await DbSet
                            .ToListAsync();

            return await DbSet
                        .IncludeMultiple(includes)
                        .ToListAsync();
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null) { return; }

            if (Context.Entry(entity).State != EntityState.Detached) { return; }

            Context.Entry(entity).State = EntityState.Modified;
            // DbSet.Update(entity);
        }

        public void Remove(object id)
        {
            var entity = Get(id);

            if (entity == null) { return; }

            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}
