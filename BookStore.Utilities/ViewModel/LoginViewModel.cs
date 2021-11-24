using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilities.ViewModel
{
	public class LoginViewModel
	{
		public string UserName { set; get; }

		public string Password { set; get; }
	}

	public class DataViewModel
	{
		public string Id { set; get; }

		public string Value { set; get; }
	}
}
