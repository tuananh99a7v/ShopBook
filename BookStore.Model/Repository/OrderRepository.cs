using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository
{
	public interface IOrderReposistory : IRepository<Order>
	{

	}
	public class OrderRepository : RepositoryBase<Order>, IOrderReposistory
	{
		public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
