using AutoMapper;
using BookStore.Model.Models;
using BookStore.Utilities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class AutoMapperConfiguration
    {
		public static MapperConfiguration Configure()
		{
			return new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AppUser, UserViewModel>();
				cfg.CreateMap<UserViewModel, AppUser>();

				cfg.CreateMap<LogHistory, LogHistoryViewModel>();
				cfg.CreateMap<LogHistoryViewModel, LogHistory>();

				cfg.CreateMap<Book, BookViewModel>();
				cfg.CreateMap<BookViewModel, Book>();

				cfg.CreateMap<Author, AuthorViewModel>();
				cfg.CreateMap<AuthorViewModel, Author>();

				cfg.CreateMap<Author, AuthorViewModel>();
				cfg.CreateMap<AuthorViewModel, Author>();

				cfg.CreateMap<Order, OrderViewModel>();
				cfg.CreateMap<OrderViewModel, Order>();

				cfg.CreateMap<OrderDetail, OrderDetailViewModel>();
				cfg.CreateMap<OrderDetailViewModel, OrderDetail>();

			});
		}
	}
}
