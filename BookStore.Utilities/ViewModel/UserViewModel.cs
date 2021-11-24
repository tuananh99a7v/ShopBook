using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilities.ViewModel
{
    public class UserViewModel
    {
        public string UserId { set; get; }
        public string FullName { set; get; }
        public string Password { set; get; }
        public string PasswordOld { set; get; }
        public string PasswordNew { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public DateTime DateCreated { set; get; }
        public bool EmailConfirmed { set; get; }
        public string Avatar { set; get; }
        public string RoleName { set; get; }
        public int NumberOfMail { get; set; }
        public byte Status { get; set; }
        public IEnumerable<string> Role { set; get; }
		public bool IsChangePass { get; set; }
	}
    /// <summary>
    /// Đối tượng User để lưu Cookie
    /// </summary>
    public class UserCookie
    {
        public string UserId { set; get; }
        public string FullName { set; get; }
        public string UserName { set; get; }
        public string Avatar { set; get; }
    }
}
