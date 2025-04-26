using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace MiniE_Commerce.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> IsExistAsync(Expression<Func<T, bool>> filter);
        Task<int> SaveChangesAsync();
        Task<T> GetEntityById(Guid id);
        Task<IEnumerable<T>> GetAllEntities();
        Task<int> CreateEntityAsync(T obj);
        Task<int> CreateEntityRangeAsync(IEnumerable<T> obj);
        Task<int> UpdateEntityAsync(T obj);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        void DeleteEntityAsync(T obj);
        void DeleteAllEntitiesAsync(IEnumerable<T> obj);
        Task<T> GetEntityWithSpec(ISpecification<T> specifications);
        Task<IEnumerable<T>> GetAllEntitiesWithSpec(ISpecification<T> specifications);
        public void DetachedEntity(T obj);
        Task ExecuteUpdateRange(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression);
        (IQueryable<T> data, int count) GetAllEntitiesAndCountWithSpec(ISpecification<T> specification);
    }
}
