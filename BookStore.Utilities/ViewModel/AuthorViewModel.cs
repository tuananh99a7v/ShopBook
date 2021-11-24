using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilities.ViewModel
{
	public class AuthorViewModel
	{
		public int AuthorId { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Description { get; set; }
		public int? QuantityImport { get; set; }
		public int? QuantityExport { get; set; }
	}
}
