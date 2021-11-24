using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository
{
	public interface ILogHistoryRepository : IRepository<LogHistory>
	{

	}
	public class LogHistoryRepository : RepositoryBase<LogHistory>, ILogHistoryRepository
	{
		public LogHistoryRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
