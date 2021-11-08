using System;

namespace BookStore.Model.Infrastructure
{
	public interface IDbFactory : IDisposable
    {
        BookStoreDbContext Init();
    }

    public class DbFactory : Disposable, IDbFactory
    {
        private BookStoreDbContext _dbContext;

        public BookStoreDbContext Init()
        {
            return _dbContext ?? (_dbContext = new BookStoreDbContext());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}