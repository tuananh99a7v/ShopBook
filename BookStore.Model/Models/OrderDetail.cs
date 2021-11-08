using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model.Models
{
	public class OrderDetail
	{
		[Key]
		[Column(Order = 1)]
		public int OrderId { get; set; }
		[Key]
		[Column(Order = 2)]
		public string UserId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public int BookId { get; set; }
	}
}
