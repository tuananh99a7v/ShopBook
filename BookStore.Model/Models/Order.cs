using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model.Models
{
	[Table("Order")]
	public class Order
	{
		public int OrderId { get; set; }
		public string ReceiverName { get; set; }
		public string Address { get; set; }
		public byte PhoneNumber { get; set; }
		public int Status { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.Now;
		public DateTime DateModified { get; set; } = DateTime.Now;
		public decimal Cost { get; set; }
	}
}
