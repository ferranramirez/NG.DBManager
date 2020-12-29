using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NG.DBManager.Infrastructure.Impl.EF.Extensions
{
    public static class QueryableExtension
    {

        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }

        public static IQueryable<T> SetPagination<T>(this DbSet<T> source, int? pageNumber, int? pageSize)
            where T : class
        {
            if (pageNumber != default || pageNumber != default)
                return source.Skip((int)pageNumber).Take((int)pageSize);

            return source;
        }

        public static IQueryable<T> SetPagination<T>(this IQueryable<T> query, int? pageNumber, int? pageSize)
            where T : class
        {
            if (pageNumber != default || pageNumber != default)
                return query.Skip((int)pageNumber).Take((int)pageSize);

            return query;
        }
    }
}
