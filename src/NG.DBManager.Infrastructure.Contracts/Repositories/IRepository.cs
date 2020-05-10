using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity Get(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(object id);
    }
}
