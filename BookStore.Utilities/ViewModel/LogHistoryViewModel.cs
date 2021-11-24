using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilities.ViewModel
{
	public class LogHistoryViewModel
	{
		public int LogHistoryId { get; set; }
		public string Content { get; set; }
		public object ObjectId { get; set; }
		public DateTime? DateCreated { get; set; } = DateTime.Now;
		public UserViewModel UserViewModel { set; get; }
	}
}
