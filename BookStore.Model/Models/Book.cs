using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model.Models
{
	[Table("Book")]
	public class Book
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
		public int AuthorId { get; set; }
		public int Category { get; set; }
		public string UserId { get; set; }

	}
}
