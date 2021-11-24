using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Model.Models
{
	[Table("Author")]
	public class Author
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
