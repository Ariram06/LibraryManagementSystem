using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book b);
        Task UpdateBookAsync(int id, Book b);
        Task<bool> DeleteBookAsync(int id);
    }
}
