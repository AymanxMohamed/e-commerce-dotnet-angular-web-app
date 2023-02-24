using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);
            //  Aggregate : Applies an accumulator function over a sequence. 
            // The specified seed value is used as the initial accumulator value.
            query = specification.Includes.Aggregate(query, (entity, includeExpression) => entity.Include(includeExpression));
            return query;
        }
    }
}