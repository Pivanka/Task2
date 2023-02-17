using BLL.Dtos;

namespace BLL.Services.Contracts
{
    public interface IReviewService
    {
        Task<int> CreateReviewAsync(int bookId, ReviewDto review);
    }
}
