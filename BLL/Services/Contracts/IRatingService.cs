using BLL.Dtos;

namespace BLL.Services.Contracts
{
    public interface IRatingService
    {
        Task<int> CreateRatingAsync(int bookId, RatingDto rating);
    }
}
