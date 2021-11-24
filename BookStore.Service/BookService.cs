using AutoMapper;
using BookStore.Model.Infrastructure;
using BookStore.Model.Models;
using BookStore.Model.Repository;
using BookStore.Utilities;
using BookStore.Utilities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Service
{
	public interface IBookService
	{
		void AddOrUpdate(BookViewModel bookViewModel);
		BookViewModel FindById(int bookId);
		int Count(string search);
		IEnumerable<BookViewModel> GetListPagination(int page, int pageSize, string search);
		void Remove(int bookId);
		int GetCountBookExportById(string bookId);
		void Save();
		Task SaveAsync();
	}
	public class BookService : IBookService
	{

		private readonly IBookRepository _bookRepository;
		private readonly IAppUserRepository _userRepository;
		private readonly IAuthorRepository _authorRepository;
		private readonly IAuthorRepository _publisherRepository;
		private readonly IOrderDetailRepository _orderDetailRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper = new Mapper(AutoMapperConfiguration.Configure());
		public BookService(IUnitOfWork unitOfWork, IBookRepository bookRepository,IAppUserRepository userRepository,IAuthorRepository authorRepository,IAuthorRepository publisherRepository,IOrderRepository orderRepository,IOrderDetailRepository orderDetailRepository)
		{
			_unitOfWork = unitOfWork;
			_bookRepository = bookRepository;
			_userRepository = userRepository;
			_authorRepository = authorRepository;
			_publisherRepository = publisherRepository;
			_orderRepository = orderRepository;
			_orderDetailRepository = orderDetailRepository;
		}
		public void AddOrUpdate(BookViewModel bookViewModel)
		{
			var book = new Book();
			if (bookViewModel.BookId > 0)//Trường hợp cập nhật
			{
				book = _bookRepository.FindById(bookViewModel.BookId);
				book.UpdateBook(bookViewModel);
				_bookRepository.Update(book);
			}
			else
			{
				book.UpdateBook(bookViewModel);
				_bookRepository.Add(book);
			}
		}

		public int Count(string search)
		{
			return (from book in _bookRepository.FindAll()
					join author in _authorRepository.FindAll() on book.AuthorId equals author.AuthorId
					join publisher in _publisherRepository.FindAll() on book.AuthorId equals publisher.AuthorId
					join user in _userRepository.FindAll() on book.UserId equals user.Id
					where (string.IsNullOrEmpty(search)
						   || book.Name.Contains(search))
					select book).Count();
		}

		public BookViewModel FindById(int bookId)
		{
			if (bookId == 0) return new BookViewModel();
			return _mapper.Map<Book,BookViewModel>(_bookRepository.FindById(bookId));
		}
		public int GetCountBookExportById(string bookId)
		{
			return (from orderDetail in _orderDetailRepository.FindAll(x => x.BookId.Equals(bookId))
					join order in _orderRepository.FindAll() on orderDetail.OrderId equals order.OrderId
					where (order.Status == 3)
					select orderDetail).Count();
		}

		public IEnumerable<BookViewModel> GetListPagination(int page, int pageSize, string search)
		{
			return (from book in _bookRepository.FindAll()
					join author in _authorRepository.FindAll() on book.AuthorId equals author.AuthorId
					join publisher in _publisherRepository.FindAll() on book.AuthorId equals publisher.AuthorId
					join user in _userRepository.FindAll() on book.UserId equals user.Id
					where (string.IsNullOrEmpty(search)
						   || book.Name.Contains(search))
					orderby book.Price descending
					select new BookViewModel
					{
						BookId=book.BookId,
						Name=book.Name,
						Price=book.Price,
						AuthorName=author.Name,
						PublisherName=publisher.Name,
						UserViewModel=new UserViewModel
						{
							UserName=user.UserName,
							Avatar=user.Avatar
						},
						UserId=book.UserId,
						Category=book.Category,
						DateCreated=book.DateCreated,
						DateModified=book.DateModified,
						Status=book.Status,
						Description=book.Description,
						Quantity=book.Quantity,
						QuantityExport=book.QuantityExport,
						QuantityImport=book.QuantityImport
						
					}).Skip((page - 1) * pageSize).Take(pageSize);
		}

		public void Remove(int bookId)
		{
			var book = _bookRepository.FindById(bookId);
			_bookRepository.Remove(book);
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
