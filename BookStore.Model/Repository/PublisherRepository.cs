using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace PublisherStore.Model.Repository
{
	public interface IPublisherReposistory : IRepository<Publisher>
	{

	}
	public class PublisherRepository : RepositoryBase<Publisher>, IPublisherReposistory
	{
		public PublisherRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
