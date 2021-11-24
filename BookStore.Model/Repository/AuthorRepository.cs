using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository

{
	public interface IAuthorRepository : IRepository<Author>
	{

	}
	public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
	{
		public AuthorRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
