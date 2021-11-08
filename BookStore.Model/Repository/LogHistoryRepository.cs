using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository
{
	public interface ILogHistoryReposistory : IRepository<LogHistory>
	{

	}
	public class LogHistoryRepository : RepositoryBase<LogHistory>, ILogHistoryReposistory
	{
		public LogHistoryRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
