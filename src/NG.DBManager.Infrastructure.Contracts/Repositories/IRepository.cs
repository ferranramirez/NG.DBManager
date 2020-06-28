using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity Get(object id);
        Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] include);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(object id);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
