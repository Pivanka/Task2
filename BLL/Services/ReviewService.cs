using AutoMapper;
using BLL.Dtos;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repositories.Contracts;
using FluentValidation;

namespace BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ReviewDto> _validator;
        private readonly IRepository<Book> _bookRepository;

        public ReviewService(IRepository<Review> reviewRepository,
            IMapper mapper,
            IValidator<ReviewDto> validator,
            IRepository<Book> bookRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _validator = validator;
            _bookRepository = bookRepository;
        }
        public async Task<int> CreateReviewAsync(int bookId, ReviewDto review)
        {
            await _bookRepository.GetByIdAsync(bookId);

            await _validator.ValidateAndThrowAsync(review);

            var reviewToAdd = _mapper.Map<Review>(review);
            reviewToAdd.BookId = bookId;

            var result = await _reviewRepository.AddAsync(reviewToAdd);
            await _reviewRepository.SaveChangesAsync();

            return result.Id;
        }
    }
}
