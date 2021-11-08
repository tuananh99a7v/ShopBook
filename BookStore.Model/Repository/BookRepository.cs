using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository
{
	public interface IBookReposistory: IRepository<Book>
	{

	}
	public class BookRepository:RepositoryBase<Book>,IBookReposistory
	{
		public BookRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
