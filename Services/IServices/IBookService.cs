using LibraryManagementSystem.DTOs;

namespace LibraryManagementSystem.Services.IServices
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<string> AddBookAsync(CreateBookDto b);
        Task<bool> UpdateBookAsync(int id, BookDto b);
        Task<bool> DeleteBookAsync(int id);
    }
}
