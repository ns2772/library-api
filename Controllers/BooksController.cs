using LibraryApi.Interfaces;
using LibraryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private IBookService bookService;

        public BooksController(IBookService _bookService)
        {
            this.bookService = _bookService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var book = await bookService.GetBooks();
            return Ok(book);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetUser(int id)
        {
            var book = await bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            var newBook = await bookService.CreateBook(book);
            return CreatedAtAction(nameof(GetUser), new { id = newBook.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var book = await bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.ISBN = updatedBook.ISBN;
            book.PublicationYear = updatedBook.PublicationYear;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            await bookService.DeleteBook(book.Id);
            return NoContent();
        }
    }
}
