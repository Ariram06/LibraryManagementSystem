using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService obj;

        public BookController(IBookService IBookServiceObj)
        {
            obj = IBookServiceObj;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var books = await obj.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookById(int id)
        {
            var book = await obj.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            return Ok(book);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> AddBook([FromBody] CreateBookDto book)
        {
            if (book == null)
            {
                return BadRequest("Book is null");
            }

            var result = await obj.AddBookAsync(book);
            if (result != "book added successfully")
            {
                return BadRequest("failed to add book");
            }

            return Ok(new { message = result });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] BookDto book)
        {
            if (book == null)
            {
                return BadRequest("Book is null");
            }

            if (id != book.BookId)
            {
                return BadRequest("Book ID mismatch");
            }

            var result = await obj.UpdateBookAsync(id, book);
            if (!result)
            {
                return NotFound("Book not found");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = await obj.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound("Book not found");
            }
            return NoContent();
        }
    }
}
