using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace AuthorStore.Model.Repository
{
	public interface IAuthorReposistory : IRepository<Author>
	{

	}
	public class AuthorRepository : RepositoryBase<Author>, IAuthorReposistory
	{
		public AuthorRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
