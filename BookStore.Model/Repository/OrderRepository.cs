using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository
{
	public interface IOrderRepository : IRepository<Order>
	{

	}
	public class OrderRepository : RepositoryBase<Order>, IOrderRepository
	{
		public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
