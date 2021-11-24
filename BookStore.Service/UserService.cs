using AutoMapper;
using BookStore.Model;
using BookStore.Model.Infrastructure;
using BookStore.Model.Models;
using BookStore.Model.Repository;
using BookStore.Utilities;
using BookStore.Utilities.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public interface IUserService
    {
        bool CheckUserName(string userName);

        void AddOrUpdate(UserViewModel userViewModel);

        int Count(string search = "");

        UserViewModel FindByUserName(string userName);

        UserViewModel FindById(string userId);

        IEnumerable<UserViewModel> Select();

        IEnumerable<UserViewModel> FillAll();

        void Remove(string userId);

        IEnumerable<UserViewModel> GetListPagination(int page, int pageSize, string search = "");

        void Save();

        Task SaveAsync();
    }
    public class UserService:IUserService
	{
        private readonly IAppUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = new Mapper(AutoMapperConfiguration.Configure());

        public UserService(IUnitOfWork unitOfWork, IAppUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public void AddOrUpdate(UserViewModel userViewModel)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(new BookStoreDbContext()));
            var user = new AppUser();
            if (!string.IsNullOrEmpty(userViewModel.UserId))
            {
                user = _userRepository.FindSingle(x => x.Id == userViewModel.UserId);
                user.UpdateUser(userViewModel);
                _userRepository.Update(user);
            }
            else
            {
                manager.Create(user, "MatKhauLan#200");
                user.UpdateUser(userViewModel);
                _userRepository.Add(user);
            }
        }

        public bool CheckUserName(string userName)
        {
            return _userRepository.CheckContains(x => x.UserName == userName);
        }

        public int Count(string search = "")
        {
            return _userRepository.Count(x => string.IsNullOrEmpty(search)
                                            || x.UserName.Contains(search)
                                            || x.FullName.Contains(search));
        }

        public UserViewModel FindByUserName(string userName)
        {
            return _mapper.Map<AppUser, UserViewModel>(_userRepository.FindSingle(x => x.UserName == userName));
        }

        public UserViewModel FindById(string userId)
        {
            return _mapper.Map<AppUser, UserViewModel>(_userRepository.FindSingle(x => x.Id == userId));
        }

        public IEnumerable<UserViewModel> Select()
        {
            return _userRepository.FindAll().Select(x => new UserViewModel
            {
                UserId = x.Id,
                UserName = x.UserName,
                FullName = x.FullName
            });
        }

        public IEnumerable<UserViewModel> FillAll()
        {
            return _userRepository.FindAll().Select(x => new UserViewModel
            {
                UserId = x.Id,
                UserName = x.UserName,
                PhoneNumber = x.PhoneNumber
            });
        }

        public IEnumerable<UserViewModel> GetListPagination(int page, int pageSize, string search = "")
        {
            return (from user in _userRepository.FindAll()
                    where string.IsNullOrEmpty(search)
                          || user.UserName.Contains(search)
                          || user.FullName.Contains(search)
                    orderby user.Id descending
                    select new UserViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        FullName = user.FullName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                    }).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Remove(string userId)
        {
            var user = _userRepository.FindSingle(x => x.Id == userId);
            _userRepository.Remove(user);
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
