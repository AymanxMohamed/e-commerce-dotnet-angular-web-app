using System.Linq.Expressions;

namespace Core.Specifications;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
{
    protected BaseSpecification()
    {
    }
    public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<TEntity, bool>> Criteria { get; }

    public List<Expression<Func<TEntity, object>>> Includes { get; } =
        new List<Expression<Func<TEntity, object>>>();

    protected void AddInclude(Expression<Func<TEntity, Object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}