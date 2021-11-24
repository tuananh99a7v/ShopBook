using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilities.ViewModel
{
	public class OrderViewModel
	{
		public int OrderId { get; set; }
		public string ReceiverName { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public int? Status { get; set; }
		public DateTime? DateCreated { get; set; } = DateTime.Now;
		public DateTime? DateModified { get; set; } = DateTime.Now;
		public decimal? Cost { get; set; }
		public string UserId { get; set; }
		public string FullName { get; set; }
		public string UserName { get; set; }
	}
}
