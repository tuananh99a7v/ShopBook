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
	public interface IAlertService
	{
		void AddOrUpdate(AlertViewModel alertViewModel);
		AlertViewModel FindById(int alertId);
		int Count(string search);
		IEnumerable<AlertViewModel> GetListPagination(int page, int pageSize, string search);
		void Remove(int alertId);
		void Save();
		Task SaveAsync();
	}
	public class AlertService : IAlertService
	{
		private readonly IAlertRepository _alertRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper = new Mapper(AutoMapperConfiguration.Configure());

		public AlertService(IUnitOfWork unitOfWork, IAlertRepository alertRepository)
		{
			_unitOfWork = unitOfWork;
			_alertRepository = alertRepository;
		}
		public void AddOrUpdate(AlertViewModel alertViewModel)
		{
			var alert = new Alert();
			if (alertViewModel.AlertId > 0)
			{
				alert = _alertRepository.FindById(alertViewModel.AlertId);
				alert.UpdateAlert(alertViewModel);
				_alertRepository.Update(alert);
			}
			else
			{
				alert.UpdateAlert(alertViewModel);
				_alertRepository.Add(alert);
			}
		}
		public int Count(string search)
		{
			return (from alert in _alertRepository.FindAll()
					where (string.IsNullOrEmpty(search) || alert.Content.Contains(search))
					select alert).Count();
		}

		public AlertViewModel FindById(int alertId)
		{
			if (alertId == 0) return new AlertViewModel();
			var alertViewModel = _mapper.Map<Alert, AlertViewModel>(_alertRepository.FindById(alertId));
			return alertViewModel;
		}

		public IEnumerable<AlertViewModel> GetListPagination(int page, int pageSize, string search)
		{
			return (from alert in _alertRepository.FindAll()
					where (string.IsNullOrEmpty(search)
						   || alert.Content.Contains(search))
					orderby alert.AlertId descending
					select new AlertViewModel
					{
						AlertId = alert.AlertId,
						Content = alert.Content,
						DateCreated = alert.DateCreated,
						DateModified = alert.DateModified,
						Status = alert.Status
					}).Skip((page - 1) * pageSize).Take(pageSize);
		}

		public void Remove(int alertId)
		{
			var alert = _alertRepository.FindById(alertId);
			if (alert == null) return;
			_alertRepository.Remove(alert);
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
