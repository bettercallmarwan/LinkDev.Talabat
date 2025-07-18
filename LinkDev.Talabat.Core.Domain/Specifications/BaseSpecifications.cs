using LinkDev.Talabat.Core.Domain.Contracts;
using System.Linq.Expressions;
using System.Security;

namespace LinkDev.Talabat.Core.Domain.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();


        public BaseSpecifications()
        {
            //Criteria = null;
        }

        public BaseSpecifications(TKey id)
        {
            Criteria = E => E.Id.Equals(id);

        }

    }
}
