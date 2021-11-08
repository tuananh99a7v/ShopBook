using BookStore.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
	public class BookStoreDbContext: IdentityDbContext<AppUser>
    {
        public BookStoreDbContext() : base("BookStoreConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Author> Authors { set; get; }
        public DbSet<Book> Books { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<LogHistory> LogHistories { get; set; }

        public static BookStoreDbContext Create()
        {
            return new BookStoreDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("AppUserRole");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("AppUserLogin");
            builder.Entity<IdentityRole>().ToTable("AppRole");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("AppUserClaim");
        }
    }
}
