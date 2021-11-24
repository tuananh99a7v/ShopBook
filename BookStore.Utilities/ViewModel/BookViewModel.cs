using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilities.ViewModel
{
	public class BookViewModel
	{
		public int BookId { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Images { get; set; }
		public decimal? Price { get; set; }
		public int? Status { get; set; }
		public int? Quantity { get; set; }
		public int? QuantityImport { get; set; }
		public int? QuantityExport { get; set; }
		public DateTime? DateCreated { get; set; } = DateTime.Now;
		public DateTime? DateModified { get; set; } = DateTime.Now;
		public int PublisherId { get; set; }
		public string PublisherName { get; set; }
		public int AuthorId { get; set; }
		public string AuthorName { get; set; }
		public int Category { get; set; }
		public string UserId { get; set; }
		public UserViewModel UserViewModel { get; set; }
	}
}
