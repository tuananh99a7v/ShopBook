using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Model.Models
{
	[Table("Alert")]
	public class Alert
	{
		public int AlertId { get; set; }
		public string Content { get; set; }
		public int Status { get; set; }
		public string UserId { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }

	}
}
