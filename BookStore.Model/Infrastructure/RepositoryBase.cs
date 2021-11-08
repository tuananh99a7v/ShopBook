using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Model.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        T FindById(int id);

        Task<T> FindByIdAsync(int id);

        T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        T FindFirst(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        void Remove(T entity);

        void Remove(int id);

        void RemoveMultiple(List<T> entities);

        T Add(T entity);

        T ExecStore(string sql, params object[] parameters);

        int ExecStoreToInt(string sql, params object[] parameters);

        Task<T> ExecStoreAsync(string sql, params object[] parameters);

        Task<int> ExecStoreToIntAsync(string sql, params object[] parameters);

        void Update(T entity);

        bool CheckContains(Expression<Func<T, bool>> predicate);

        Task<bool> CheckContainsAsync(Expression<Func<T, bool>> predicate);

        int Count(Expression<Func<T, bool>> where);

        Task<int> CountAsync(Expression<Func<T, bool>> where);
    }

    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private BookStoreDbContext _dataContext;
        private readonly IDbSet<T> _dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected BookStoreDbContext DbContext => _dataContext ?? (_dataContext = DbFactory.Init());

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = DbContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = DbContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }

        public T FindById(int id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(predicate);
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindAll(includeProperties).SingleOrDefaultAsync(predicate);
        }

        public T FindFirst(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties).FirstOrDefault(predicate);
        }

        public async Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindAll(includeProperties).FirstOrDefaultAsync(predicate);
        }

        public void Remove(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }

        public void Remove(int id)
        {
            var entity = FindById(id);
            Remove(entity);
        }

        public void RemoveMultiple(List<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
        }

        public T Add(T entity)
        {
            return DbContext.Set<T>().Add(entity);
        }

        public T ExecStore(string sql, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }

        public int ExecStoreToInt(string sql, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<int>(sql, parameters).FirstOrDefault();
        }

        public async Task<T> ExecStoreAsync(string sql, params object[] parameters)
        {
            return await DbContext.Database.SqlQuery<T>(sql, parameters).FirstOrDefaultAsync();
        }

        public async Task<int> ExecStoreToIntAsync(string sql, params object[] parameters)
        {
            return await DbContext.Database.SqlQuery<int>(sql, parameters).FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.Set<T>().Count<T>(predicate) > 0;
        }

        public async Task<bool> CheckContainsAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().CountAsync<T>(predicate) > 0;
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return DbContext.Set<T>().Count(where);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> @where)
        {
            return await DbContext.Set<T>().CountAsync(where);
        }
	}
}