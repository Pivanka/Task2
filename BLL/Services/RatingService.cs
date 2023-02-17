using AutoMapper;
using BLL.Dtos;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repositories.Contracts;

namespace BLL.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<Rating> _ratingRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public RatingService(IRepository<Rating> ratingRepository, 
            IMapper mapper, 
            IRepository<Book> bookRepository)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }
        public async Task<int> CreateRatingAsync(int bookId, RatingDto rating)
        {
            await _bookRepository.GetByIdAsync(bookId);

            var ratingToAdd = _mapper.Map<Rating>(rating);
            ratingToAdd.BookId = bookId;

            var result = await _ratingRepository.AddAsync(ratingToAdd);
            await _ratingRepository.SaveChangesAsync();

            return result.Id;
        }
    }
}
