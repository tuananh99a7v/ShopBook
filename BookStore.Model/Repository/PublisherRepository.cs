using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository
{
	public interface IPublisherRepository : IRepository<Publisher>
	{

	}
	public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository
	{
		public PublisherRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
