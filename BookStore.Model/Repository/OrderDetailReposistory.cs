using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository
{
	public interface IOrderDetailRepository : IRepository<OrderDetail>
	{

	}
	public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
	{
		public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
