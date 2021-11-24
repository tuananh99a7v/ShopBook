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
	public interface IOrderService
	{
		void AddOrUpdate(OrderViewModel orderViewModel);
		OrderViewModel FindById(int orderId);
		int Count(string search);
		IEnumerable<OrderViewModel> GetListPagination(int page, int pageSize, string search);
		void Remove(int orderId);
		void Save();
		Task SaveAsync();
	}
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IAppUserRepository _appUserRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper = new Mapper(AutoMapperConfiguration.Configure());

		public OrderService(IUnitOfWork unitOfWork,IOrderRepository orderRepository, IAppUserRepository userRepository)
		{
			_unitOfWork = unitOfWork;
			_appUserRepository = userRepository;
			_orderRepository = orderRepository;
		}
		public void AddOrUpdate(OrderViewModel orderViewModel)
		{
			var order = new Order();
			if (orderViewModel.OrderId > 0)
			{
				order = _orderRepository.FindById(orderViewModel.OrderId);
				order.UpdateOrder(orderViewModel);
				_orderRepository.Update(order);
			}
			else
			{
				order.UpdateOrder(orderViewModel);
				_orderRepository.Add(order);
			}
		}
		public int Count(string search)
		{
			return (from order in _orderRepository.FindAll()
					join user in _appUserRepository.FindAll() on order.UserId equals user.Id
					where (string.IsNullOrEmpty(search) || user.UserName.Contains(search) || user.FullName.Contains(search))
					select order).Count();
		}

		public OrderViewModel FindById(int orderId)
		{
			if (orderId == 0) return new OrderViewModel();
			var orderViewModel = _mapper.Map<Order, OrderViewModel>(_orderRepository.FindById(orderId));
			return orderViewModel;
		}

		public IEnumerable<OrderViewModel> GetListPagination(int page, int pageSize, string search)
		{
			return (from order in _orderRepository.FindAll()
					join user in _appUserRepository.FindAll() on order.UserId equals user.Id
					where (string.IsNullOrEmpty(search) || user.UserName.Contains(search) || user.FullName.Contains(search))
					orderby order.Cost descending
					select new OrderViewModel
					{
						OrderId = order.OrderId,
						UserId=user.Id,
						Address=order.Address,
						PhoneNumber=user.PhoneNumber,
						Status=order.Status,
						Cost=order.Cost,
						DateCreated=order.DateCreated,
						DateModified=order.DateModified,
						ReceiverName=order.ReceiverName,
						UserName=user.UserName,
						FullName=user.FullName
					}).Skip((page - 1) * pageSize).Take(pageSize);
		}

		public void Remove(int orderId)
		{
			var order = _orderRepository.FindById(orderId);
			if (order == null) return;
			_orderRepository.Remove(order);
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
