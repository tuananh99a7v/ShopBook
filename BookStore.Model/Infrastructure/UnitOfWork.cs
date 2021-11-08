using System.Threading.Tasks;

namespace BookStore.Model.Infrastructure
{
    public interface IUnitOfWork
    {
        void Save();

        Task SaveAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private BookStoreDbContext _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public BookStoreDbContext DbContext => _dbContext ?? (_dbContext = _dbFactory.Init());

        public void Save()
        {
            DbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}