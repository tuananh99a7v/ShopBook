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
	public interface IAuthorService
	{
		void AddOrUpdate(AuthorViewModel authorViewModel);
		AuthorViewModel FindById(int authorId);
		int Count(string search);
		IEnumerable<AuthorViewModel> GetListPagination(int page, int pageSize, string search);
		void Remove(int authorId);
		void Save();
		Task SaveAsync();
	}
	public class AuthorService : IAuthorService
	{
		private readonly IAuthorRepository _authorRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper = new Mapper(AutoMapperConfiguration.Configure());

		public AuthorService(IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
		{
			_unitOfWork = unitOfWork;
			_authorRepository = authorRepository;
		}
		public void AddOrUpdate(AuthorViewModel authorViewModel)
		{
			var author = new Author();
			if (authorViewModel.AuthorId > 0)
			{
				author = _authorRepository.FindById(authorViewModel.AuthorId);
				author.UpdateAuthor(authorViewModel);
				_authorRepository.Update(author);
			}
			else
			{
				author.UpdateAuthor(authorViewModel);
				_authorRepository.Add(author);
			}
		}
		public int Count(string search)
		{
			return (from author in _authorRepository.FindAll()
					where (string.IsNullOrEmpty(search) || author.Name.Contains(search))
					select author).Count();
		}

		public AuthorViewModel FindById(int authorId)
		{
			if (authorId == 0) return new AuthorViewModel();
			var authorViewModel = _mapper.Map<Author, AuthorViewModel>(_authorRepository.FindById(authorId));
			return authorViewModel;
		}

		public IEnumerable<AuthorViewModel> GetListPagination(int page, int pageSize, string search)
		{
			return (from author in _authorRepository.FindAll()
					where (string.IsNullOrEmpty(search)
						   || author.Name.Contains(search))
					orderby author.Name descending
					select new AuthorViewModel
					{
						AuthorId = author.AuthorId,
						Name = author.Name,
						Description = author.Description,
						QuantityExport = author.QuantityExport,
						QuantityImport = author.QuantityImport,
						Address=author.Address,
						Email=author.Email,
						PhoneNumber=author.PhoneNumber,
						
					}).Skip((page - 1) * pageSize).Take(pageSize);
		}

		public void Remove(int authorId)
		{
			var author = _authorRepository.FindById(authorId);
			if (author == null) return;
			_authorRepository.Remove(author);
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
