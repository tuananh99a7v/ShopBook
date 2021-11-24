using BookStore.Model.Infrastructure;
using BookStore.Model.Models;
using BookStore.Model.Repository;
using BookStore.Utilities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BookStore.Service
{
	public interface ILogHistoryService
    {
        int Add(LogHistory logHistory);

        IQueryable<LogHistory> GetAllByUserId(string userId);

        IQueryable<LogHistoryViewModel> GetAllByUserIdClient(string userId, int size);

        int Count(string search = "", string userId = "");

        IEnumerable<LogHistoryViewModel> GetListPagination(int page, int pageSize, string search = "", string userId = "");

        void Save();

        Task SaveAsync();
    }
    public class LogHistoryService: ILogHistoryService
	{
        private readonly ILogHistoryRepository _logHistoryRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogHistoryService(ILogHistoryRepository logHistoryRepository, IUnitOfWork unitOfWork, IAppUserRepository appUserRepository)
        {
            _logHistoryRepository = logHistoryRepository;
            _unitOfWork = unitOfWork;
            _appUserRepository = appUserRepository;
        }

		public int Add(LogHistory logHistory)
		{
			return _logHistoryRepository.Add(logHistory).LogHistoryId;
		}

		public int Count(string search = "", string userId = "")
		{
			return (from logHistory in _logHistoryRepository.FindAll()
					join appUser in _appUserRepository.FindAll() on logHistory.UserId equals appUser.Id
					where (string.IsNullOrEmpty(userId) || logHistory.UserId == userId)
						  && (string.IsNullOrEmpty(search)
							  || logHistory.Content.Contains(search))
					select logHistory).Count();
		}

		public IQueryable<LogHistory> GetAllByUserId(string userId)
		{
			return _logHistoryRepository.FindAll(x => x.UserId == userId);
		}

		public IQueryable<LogHistoryViewModel> GetAllByUserIdClient(string userId, int size)
		{
			return (from logHistory in _logHistoryRepository.FindAll()
					where logHistory.UserId == userId
					orderby logHistory.LogHistoryId descending
					select new LogHistoryViewModel
					{
						LogHistoryId = logHistory.LogHistoryId,
						Content = logHistory.Content,
						DateCreated = logHistory.DateCreated,
					}).Take(size);
		}

		public IEnumerable<LogHistoryViewModel> GetListPagination(int page, int pageSize, string search = "", string userId = "")
		{
			return (from logHistory in _logHistoryRepository.FindAll()
					join appUser in _appUserRepository.FindAll() on logHistory.UserId equals appUser.Id
					//into temp from last in temp.DefaultIfEmpty()
					where (string.IsNullOrEmpty(userId) || logHistory.UserId == userId)
						  && (string.IsNullOrEmpty(search)
							  || logHistory.Content.Contains(search))
					orderby logHistory.LogHistoryId descending
					select new LogHistoryViewModel
					{
						LogHistoryId = logHistory.LogHistoryId,
						Content = logHistory.Content,
						DateCreated = logHistory.DateCreated,
						UserViewModel = new UserViewModel
						{
							UserName = appUser.UserName,
							Avatar = appUser.Avatar,
						}
					}).Skip((page - 1) * pageSize).Take(pageSize);
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
