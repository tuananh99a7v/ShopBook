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
	public interface IPublisherService
	{
		void AddOrUpdate(PublisherViewModel publisherViewModel);
		PublisherViewModel FindById(int publisherId);
		int Count(string search);
		IEnumerable<PublisherViewModel> GetListPagination(int page, int pageSize, string search);
		void Remove(int publisherId);
		void Save();
		Task SaveAsync();
	}
	public class PublisherService : IPublisherService
	{
		private readonly IPublisherRepository _publisherRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper = new Mapper(AutoMapperConfiguration.Configure());

		public PublisherService(IUnitOfWork unitOfWork, IPublisherRepository publisherRepository)
		{
			_unitOfWork = unitOfWork;
			_publisherRepository = publisherRepository;
		}
		public void AddOrUpdate(PublisherViewModel publisherViewModel)
		{
			var publisher = new Publisher();
			if (publisherViewModel.PublisherId > 0)
			{
				publisher = _publisherRepository.FindById(publisherViewModel.PublisherId);
				publisher.UpdatePublisher(publisherViewModel);
				_publisherRepository.Update(publisher);
			}
		}
		public int Count(string search)
		{
			return (from publisher in _publisherRepository.FindAll()
					where (string.IsNullOrEmpty(search) || publisher.Name.Contains(search))
					select publisher).Count();
		}

		public PublisherViewModel FindById(int publisherId)
		{
			if (publisherId == 0) return new PublisherViewModel();
			var publisherViewModel = _mapper.Map<Publisher, PublisherViewModel>(_publisherRepository.FindById(publisherId));
			return publisherViewModel;
		}

		public IEnumerable<PublisherViewModel> GetListPagination(int page, int pageSize, string search)
		{
			return (from publisher in _publisherRepository.FindAll()
					where (string.IsNullOrEmpty(search)
						   || publisher.Name.Contains(search))
					orderby publisher.Name descending
					select new PublisherViewModel
					{
						PublisherId=publisher.PublisherId,
						Name=publisher.Name,
						Description=publisher.Description,
						QuantityExport=publisher.QuantityExport,
						QuantityImport=publisher.QuantityImport
					}).Skip((page - 1) * pageSize).Take(pageSize);
		}

		public void Remove(int publisherId)
		{
			var publisher = _publisherRepository.FindById(publisherId);
			if (publisher == null) return;
			_publisherRepository.Remove(publisher);
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
