using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MiniE_Commerce.BLL.Interfaces;
using MiniE_Commerce.BLL.Specifications;
using MiniE_Commerce.DAL.Data;
using System.Linq.Expressions;

namespace MiniE_Commerce.BLL.Repositories
{
    public class GenericRepository<T>(MiniE_CommerceDbContext context) : IGenericRepository<T> where T : class
    {
        private readonly MiniE_CommerceDbContext _context = context;

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> filter)
          => await _context.Set<T>().AnyAsync(filter);

        public async Task<int> CreateEntityAsync(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreateEntityRangeAsync(IEnumerable<T> obj)
        {
            await _context.Set<T>().AddRangeAsync(obj);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateEntityAsync(T obj)
        {
            _context.Set<T>().Update(obj);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public void DeleteAllEntitiesAsync(IEnumerable<T> obj)
        {
            _context.Set<T>().RemoveRange(obj);
        }

        public void DeleteEntityAsync(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public void DetachedEntity(T obj)
        {
            _context.Entry(obj).State = EntityState.Detached;
        }

        public async Task<IEnumerable<T>> GetAllEntities()
            => await _context.Set<T>().ToListAsync();

        public async Task<T> GetEntityById(Guid id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public async Task<IEnumerable<T>> GetAllEntitiesWithSpec(ISpecification<T> specifications)
            => await ApplySpecifications(specifications).data.ToListAsync();

        public (IQueryable<T> data, int count) GetAllEntitiesAndCountWithSpec(ISpecification<T> specifications)
                => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specifications);

        public async Task<T> GetEntityWithSpec(ISpecification<T> specifications)
            => await ApplySpecifications(specifications).data.FirstOrDefaultAsync();

        public async Task ExecuteUpdateRange(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression)
        => await _context.Set<T>()
                .Where(filter)
                .ExecuteUpdateAsync(expression);
        private (IQueryable<T> data, int count) ApplySpecifications(ISpecification<T> specifications)
            => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specifications);
    }
}