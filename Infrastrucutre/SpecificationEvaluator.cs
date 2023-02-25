using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(
            IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
        {
            var query = inputQuery;
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);
            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
            if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);
            if (specification.IsPagingEnabled)
                query = query.Skip(specification.Skip).Take(specification.Take);
            //  Aggregate : Applies an accumulator function over a sequence. 
            // The specified seed value is used as the initial accumulator value.
            query = specification.Includes.Aggregate(query, (entity, includeExpression) => entity.Include(includeExpression));
            return query;
        }
    }
}