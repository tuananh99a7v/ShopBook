using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilities.ViewModel
{
	public class OrderDetailViewModel
	{
		public int OrderId { get; set; }
		public string UserId { get; set; }
		public int Quantity { get; set; } = 0;
		public decimal UnitPrice { get; set; } = 0;
		public int BookId { get; set; }
		public string BookName { get; set; }
	}
}
