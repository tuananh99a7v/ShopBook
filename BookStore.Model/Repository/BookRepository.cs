using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository
{
	public interface IBookRepository: IRepository<Book>
	{

	}
	public class BookRepository:RepositoryBase<Book>,IBookRepository
	{
		public BookRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
