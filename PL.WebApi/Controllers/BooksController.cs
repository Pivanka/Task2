using BLL.Dtos;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace PL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IReviewService _reviewService;
        private readonly IRatingService _ratingService;

        public BooksController(IBookService bookService, 
            IReviewService reviewService, 
            IRatingService ratingService)
        {
            _bookService = bookService;
            _reviewService = reviewService;
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string? order)
        {
            var books = await _bookService.GetAllBooksAsync(order);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDetails>> GetBookDetails(int id)
        {
            var book = await _bookService.GetBookDetailsAsync(id);
            return Ok(book);
        }

        //POST https://{{baseUrl}}/api/books/save
        [HttpPost("save")]
        public async Task<ActionResult<int>> SaveBook([FromBody] AddBook book)
        {
            var bookId = await _bookService.CreateBookAsync(book);
            return Ok(bookId);
        }

        //[HttpDelete]//("{id:int}/{secret}")
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteBook([FromRoute] int id, [FromQuery] string secret)
        {
            var bookId = await _bookService.DeleteBookAsync(id, secret);
            return Ok(bookId);
        }

        //PUT https:{{baseUrl}}/api/books/{id}/review
        [HttpPut("{id}/review")]
        public async Task<ActionResult<int>> Review(int id, [FromBody] ReviewDto review)
        {
            var reviewId = await _reviewService.CreateReviewAsync(id, review);
            return Ok(reviewId);
        }

        //PUT https:{{baseUrl}}/api/books/{id}/rate
        [HttpPut("{id}/rate")]
        public async Task<ActionResult<int>> Rate(int id, [FromBody] RatingDto rate)
        {
            var ratingId = await _ratingService.CreateRatingAsync(id, rate);
            return Ok(ratingId);
        }
    }
}
