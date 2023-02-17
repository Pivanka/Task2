using AutoMapper;
using BLL.Dtos;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repositories.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IValidator<AddBook> _validator;

        public BookService(IRepository<Book> bookRepository, 
            IMapper mapper, 
            IConfiguration configuration,
            IValidator<AddBook> validator)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _configuration = configuration;
            _validator = validator;
        }
        public async Task<int> CreateBookAsync(AddBook book)
        {
            await _validator.ValidateAndThrowAsync(book);

            var bookToAdd = _mapper.Map<Book>(book);
            Book addedBook = new Book();
            if (book.Id != null)
            {
                addedBook = await _bookRepository.UpdateAsync(bookToAdd);
            }
            else
            {
                addedBook = await _bookRepository.AddAsync(bookToAdd);
            }

            await _bookRepository.SaveChangesAsync();

            return addedBook.Id;
        }

        public async Task<int> DeleteBookAsync(int id, string secret)
        {
            Book deletedBook = new Book();
            if (secret == _configuration["SecretSettings:SecretKey"])
            {
                var bookToDelete = await _bookRepository.GetByIdAsync(id);

                if (bookToDelete == null) throw new ArgumentException("Book for delete not found");

                deletedBook = await _bookRepository.RemoveAsync(bookToDelete);
                await _bookRepository.SaveChangesAsync();
            }
            return deletedBook.Id;
        }

        public async Task<BookDetails> GetBookDetailsAsync(int id)
        {
            var book = await _bookRepository
                .Query()
                .Include(b => b.Ratings).Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) throw new ArgumentException();

            return _mapper.Map<BookDetails>(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(string? order)
        {
            var books = await _bookRepository
                .Query()
                .Include(b => b.Ratings).Include(b => b.Reviews)
                .ToListAsync();

            if (order == "author")
            {
                books = books.OrderBy(b => b.Author).ToList();
            }
            else if(order == "title")
            {
                books = books.OrderBy(o => o.Title).ToList();
            }

            return _mapper.ProjectTo<BookDto>(books.AsQueryable());
        }

        public async Task<IEnumerable<BookDto>> GetTopBooksAsync(string? filter)
        {
            var books = await _bookRepository.Query().
                Include(b => b.Ratings)
                .Include(b => b.Reviews)
                .ToListAsync();

            if (filter != null)
            {
                books = books.Where(x => x.Genre == filter).ToList();
            }

            var mappedBooks = _mapper.ProjectTo<BookDto>(books.AsQueryable());

            var result = mappedBooks
                .Where(b => b.ReviewsNumber >= 10)
                .OrderByDescending(b => b.Rating)
                .Take(10)
                .ToList();
            return result;
        }
    }
}
