using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilities.ViewModel
{
	public class PublisherViewModel
	{
		public int PublisherId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int? QuantityImport { get; set; }
		public int? QuantityExport { get; set; }
	}
}
