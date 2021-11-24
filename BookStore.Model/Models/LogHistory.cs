using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model.Models
{
	public class LogHistory
	{
		public int LogHistoryId { get; set; }
		public string Content { get; set; }
		public object ObjectId { get; set; }
		public DateTime? DateCreated { get; set; } = DateTime.Now;
		public string UserId { get; set; }
	}
}
