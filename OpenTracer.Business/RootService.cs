using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using OpenTracer.Core.Abstraction;
using System.Linq.Expressions;

namespace OpenTracer.Business
{
    public class RootService<T> : IRepository<T> where T : class, IEntityRoot
    {
        private readonly IRepository<T> _repository;
        public RootService(IRepository<T> repository)
        {
            _repository = repository;
        }
        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public void Delete(List<T> entities)
        {
            _repository.Delete(entities);
        }

        public async Task<T?> Get(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includedNavigations = null)
        {
            return await _repository.Get(id, includedNavigations);
        }

        public T? Get(Expression<Func<T, bool>> query, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includedNavigations = null)
        {
            return _repository.Get(query, includedNavigations);
        }

        public async Task<T?> GetAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includedNavigations = null)
        {
            return await _repository.GetAsync(id, includedNavigations);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> query, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includedNavigations = null)
        {
            return await _repository.GetAsync(query, includedNavigations);
        }

        public T Insert(T entity)
        {
            return _repository.Insert(entity);
        }

        public List<T> Insert(List<T> entities)
        {
            return _repository.Insert(entities);
        }

        public async Task<T?> InsertAsync(T entity)
        {
            return await _repository.InsertAsync(entity);
        }

        public async Task InsertAsync(List<T> entity)
        {
            await _repository.InsertAsync(entity);
        }

        public IQueryable<T?> Query()
        {
            return _repository.Query();
        }

        public DbSet<TResult> QueryEntity<TResult>() where TResult : class
        {
            return _repository.QueryEntity<TResult>();
        }

        public int SaveChanges()
        {
            return _repository.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        public EntityEntry<T> Update(T entity)
        {
            return _repository.Update(entity);
        }

        public void Update(List<T> entities)
        {
            _repository.Update(entities);
        }
    }
}
