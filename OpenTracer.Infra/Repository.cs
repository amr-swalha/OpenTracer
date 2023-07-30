using Ardalis.GuardClauses;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using OpenTracer.Core.Abstraction;
using System.Linq.Expressions;

namespace OpenTracer.Infra
{
    public class Repository<T> : IRepository<T> where T : class, IEntityRoot
    {
        private readonly AppDbContext _dataContext;
        private readonly AppDbConnection _appDbConnection;
        public Repository(AppDbContext dataContext, AppDbConnection appDbConnection)
        {
            _dataContext = dataContext;
            Guard.Against.Null(_dataContext);
            _appDbConnection = appDbConnection;
        }

        public void Delete(T entity)
        {
            Guard.Against.Null(entity, nameof(entity), "Please provide an entity");
            _dataContext.Remove(entity);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            Guard.Against.NullOrEmpty(id);
            using (var txn = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    var item = Get(y => y.Id == id);
                    _dataContext.Remove(item);
                    SaveChanges();
                    txn.Commit();
                }
                catch (Exception)
                {
                    txn.Rollback();
                }
            }
            
        }

        public void Delete(List<T> entities)
        {
            Guard.Against.Null(entities);
            _dataContext.RemoveRange(entities);
            SaveChanges();
        }

        public async Task<T?> Get(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? navigations = null)
        {
            Guard.Against.NullOrEmpty(id);
            var query = _dataContext.Set<T>().AsQueryable();
            if (navigations != null)
            {
                query = navigations.Invoke(query);
            }
            return query.FirstOrDefault(y => y.Id == id);
        }

        public T? Get(Expression<Func<T, bool>> query, Func<IQueryable<T>, IIncludableQueryable<T, object>>? navigations = null)
        {
            Guard.Against.Null(query);
            var querable = _dataContext.Set<T>().AsQueryable();
            if (navigations != null)
            {
                querable = navigations.Invoke(querable);
            }
            return querable.FirstOrDefault(query);
        }

        public async Task<T?> GetAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? navigations = null)
        {
            Guard.Against.NullOrEmpty(id);
            var query = _dataContext.Set<T>().AsQueryable();
            if (navigations != null)
            {
                query = navigations.Invoke(query);
            }
            return await query.FirstOrDefaultAsync(y => y.Id == id);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> query, Func<IQueryable<T>, IIncludableQueryable<T, object>>? navigations = null)
        {
            Guard.Against.Null(query);
            var querable = _dataContext.Set<T>().AsQueryable();
            if (navigations != null)
            {
                querable = navigations.Invoke(querable);
            }
            return await querable.FirstOrDefaultAsync(query);
        }


        public T Insert(T entity)
        {
            Guard.Against.Null(entity);
            _dataContext.Add(entity);
            SaveChanges();
            return entity;
        }

        public List<T> Insert(List<T> entity)
        {
            Guard.Against.NullOrEmpty(entity);
            var db = _dataContext.CreateLinqToDBConnection();
            var result = db.BulkCopy(new BulkCopyOptions { }, entity);
            return entity;
        }

        public async Task InsertAsync(List<T> entity)
        {
            var result = await _dataContext.BulkCopyAsync(new BulkCopyOptions { }, entity);
        }

        public async Task<T?> InsertAsync(T entity)
        {
            Guard.Against.Null(entity);
            await _dataContext.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }



        public IQueryable<T?> Query()
        {
            return _dataContext.Set<T>().AsQueryable();
        }

        public EntityEntry<T> Update(T entity)
        {
            Guard.Against.Null(entity);
            var entry = _dataContext.Update(entity);
            SaveChanges();
            return entry;
        }

        public void Update(List<T> entities)
        {
            Guard.Against.NullOrEmpty(entities);
            _dataContext.UpdateRange(entities);
            SaveChanges();

        }


        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }

        public DbSet<TResult> QueryEntity<TResult>() where TResult : class
        {
            return _dataContext.Set<TResult>();
        }
    }
}
