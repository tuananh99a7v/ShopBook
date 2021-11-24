using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilities.ViewModel
{
	public class AlertViewModel
	{
		public int AlertId { get; set; }
		public string Content { get; set; }
		public int Status { get; set; }
		public string UserId { get; set; }
		public DateTime DateCreated { get; set; } 
		public DateTime DateModified { get; set; } 
	}
}
