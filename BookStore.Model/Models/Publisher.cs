using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model.Models
{
	
	[Table("Publisher")]
	public class Publisher
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PublisherId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
