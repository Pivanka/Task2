using BLL.Dtos;

namespace BLL.Services.Contracts
{
    public interface IBookService
    {
        Task<int> CreateBookAsync(AddBook book);
        Task<IEnumerable<BookDto>> GetAllBooksAsync(string? order);
        Task<BookDetails> GetBookDetailsAsync(int id);
        Task<int> DeleteBookAsync(int id, string secret);
        Task<IEnumerable<BookDto>> GetTopBooksAsync(string? filter);
    }
}
