using BLL.Dtos;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace PL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendedController : ControllerBase
    {
        private readonly IBookService _bookService;
        public RecommendedController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetTopBooks(string? filter)
        {
            var books = await _bookService.GetTopBooksAsync(filter);

            return Ok(books);
        }
    }
}
