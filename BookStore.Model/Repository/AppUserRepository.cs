using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository
{
	public interface IAppUserRepository : IRepository<AppUser>
	{
	}

	public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
	{
		public AppUserRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
