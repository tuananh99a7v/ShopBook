using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model.Models
{
    [Table("AppUser")]
    public class AppUser : IdentityUser
    {
        public string FullName { set; get; }
        public DateTime DateCreated { set; get; } = DateTime.Now;
        public DateTime DateModified { set; get; } = DateTime.Now;
        public string Avatar { set; get; } = "/";
        public bool IsChangePass { set; get; } = false;
        public byte Status { set; get; } = 1;
        public string Mail { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
