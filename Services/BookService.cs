using LibraryApi.Infrastructure;
using LibraryApi.Interfaces;
using LibraryApi.Models;

namespace LibraryApi.Services
{
    public class BookService : IBookService
    {
        private IRepository<Book> _bookRepository;
        public BookService(IRepository<Book> bookRepository)
        {
            this._bookRepository = bookRepository;
        }
        public async Task<List<Book>> GetBooks()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> CreateBook(Book book)
        {
            return await _bookRepository.AddAsync(book);
        }

        public async Task<Book> UpdateBook(int id, Book updatedBook)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.ISBN = updatedBook.ISBN;
                book.PublicationYear = updatedBook.PublicationYear;
                await _bookRepository.UpdateAsync(book);
            }
            return book;
        }

        public async Task DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                await _bookRepository.DeleteAsync(book);
            }
        }
    }
}
