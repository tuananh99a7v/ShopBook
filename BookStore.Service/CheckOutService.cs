using AutoMapper;
using BookStore.Model.Infrastructure;
using BookStore.Model.Models;
using BookStore.Model.Repository;
using BookStore.Utilities;
using BookStore.Utilities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
	public interface ICheckOutService
	{
		bool CheckQuantity(int bookId, int quantityBuy);
		void CheckOut(OrderViewModel orderViewModel, List<OrderDetailViewModel> orderDetailViewModels);
		void Save();
		Task SaveAsync();
	}
	public class CheckOutService : ICheckOutService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderDetailRepository _orderDetailRepository;
		private readonly IBookRepository _bookRepository;
		private readonly IMapper _mapper = new Mapper(AutoMapperConfiguration.Configure());
		public CheckOutService(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IBookRepository bookRepository)
		{
			_unitOfWork = unitOfWork;
			_orderRepository = orderRepository;
			_orderDetailRepository = orderDetailRepository;
			_bookRepository = bookRepository;
		}

		public void CheckOut(OrderViewModel orderViewModel, List<OrderDetailViewModel> orderDetailViewModels)
		{
			var book = new Book();
			var order = new Order();
			order.UpdateOrder(orderViewModel);
			_orderRepository.Add(order);
			_unitOfWork.Save();
			foreach (var item in orderDetailViewModels)
			{
				var orderDetail = new OrderDetail();
				orderDetail.UpdateOrderDetail(item);
				UpdateQuantity(item.BookId, item.Quantity);
				_orderDetailRepository.Update(orderDetail);
			}
		}
		public bool CheckQuantity(int bookId, int quantityBuy)//Kiểm tra xem còn hàng trong kho không
		{
			var book = new Book();
			if (bookId == 0) return false;
			book = _bookRepository.FindById(bookId);
			if (book.Quantity < quantityBuy) return false;
			return true;
		}
		public void UpdateQuantity(int bookId, int quantityBuy)
		{
			var book = new Book();
			book = _bookRepository.FindById(bookId);
			book.Quantity -= quantityBuy;
			book.DateModified = DateTime.Now;
			_bookRepository.Update(book);
		}

		public void Save()
		{
			_unitOfWork.Save();
		}

		public async Task SaveAsync()
		{
			await _unitOfWork.SaveAsync();
		}
	}
}
