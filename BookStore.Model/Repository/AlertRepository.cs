using BookStore.Model.Infrastructure;
using BookStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model.Repository
{
	public interface IAlertRepository : IRepository<Alert>
	{
	}

	public class AlertRepository : RepositoryBase<Alert>, IAlertRepository
	{
		public AlertRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
