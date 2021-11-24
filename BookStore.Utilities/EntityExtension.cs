using BookStore.Model.Models;
using BookStore.Utilities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilities
{
	public static class EntityExtension
	{
		public static void UpdateBook(this Book book)
		{

		}
		public static void UpdateUser(this AppUser user,UserViewModel userViewModel)
		{
			
			if(string.IsNullOrEmpty(userViewModel.UserId))
			{
				//DateCreated và DateModified đã đặt mặc định, không cần phải đặt lại
				//Trường hợp thêm mới, không có gì xảy ra
			}
			else
			{
				user.DateModified = DateTime.Now;
				user.Status = 2;
			}
			user.IsChangePass = userViewModel.IsChangePass;
			user.Email = userViewModel.Email;
			user.FullName = userViewModel.FullName;
			user.UserName = userViewModel.UserName;
			user.Avatar = userViewModel.Avatar;
		}
		public static void UpdateBook(this Book book, BookViewModel bookViewModel)
		{

			
		}
		public static void UpdatePublisher(this Publisher publisher, PublisherViewModel publisherViewModel)
		{


		}
		public static void UpdateAuthor(this Author author, AuthorViewModel authorViewModel)
		{


		}
		public static void UpdateOrderDetail(this OrderDetail orderDetail, OrderDetailViewModel orderDetailViewModel)
		{


		}
		public static void UpdateOrder(this Order order, OrderViewModel bookViewModel)
		{


		}
		public static void UpdateAlert(this Alert alert, AlertViewModel alertViewModel)
		{


		}
	}
}
