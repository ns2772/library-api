using LibraryApi.Models;

namespace LibraryApi.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task<Book> CreateBook(Book book);
        Task<Book> UpdateBook(int id, Book updatedBook);
        Task DeleteBook(int id);
    }
}
