using Microsoft.EntityFrameworkCore;
using MiniE_Commerce.BLL.Interfaces;

namespace MiniE_Commerce.BLL.Specifications
{
    public class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static (IQueryable<TEntity> data, int count) GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);

            if (specification.OrderByDesc != null)
                query = query.OrderByDescending(specification.OrderByDesc);

            if (specification.IsPaging)
                query = query.Skip(specification.Skip).Take(specification.Take);

            if (specification.DependenceyIncludes.Any())
                specification.DependenceyIncludes.ForEach(x => query = query.Include(x));

            query = specification.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            int count = query.Count();

            return (query, count);
        }
    }
}
