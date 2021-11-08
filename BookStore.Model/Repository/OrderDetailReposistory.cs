using BookStore.Model.Infrastructure;
using BookStore.Model.Models;

namespace BookStore.Model.Repository
{
	public interface IOrderDetailReposistory : IRepository<OrderDetail>
	{

	}
	public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailReposistory
	{
		public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
