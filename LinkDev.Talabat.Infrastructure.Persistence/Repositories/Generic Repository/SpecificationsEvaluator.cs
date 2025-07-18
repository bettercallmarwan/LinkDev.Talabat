using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository
{
    internal static class SpecificationsEvaluator<TEntity, TKey>
           where TEntity : BaseEntity<TKey>
           where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery; //_dbContext.Set<Product>();

            if (spec.Criteria is not null) // p => p.ID.equals(1)
                query = query.Where(spec.Criteria);

            // _dbContext.Set<Product>().where( p => p.ID.equals(1))

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }

}




















//internal static class SpecificationsEvaluator<TEntity, TKey>
//       where TEntity : BaseEntity<TKey>
//       where TKey : IEquatable<TKey>
//{
//    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
//    {
//        var query = inputQuery; //_dbContext.Set<Product>();

//        if (spec.Criteria is not null) // p => p.ID.equals(1)
//            query = query.Where(spec.Criteria);

//        // _dbContext.Set<Product>().where( p => p.ID.equals(1))

//        query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

//        return query;
//    }
//}
