using LibraryManagementSystem.Models;
using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services.IServices;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        protected readonly IBookRepository obj;

        public BookService(IBookRepository BookRepoObj)
        {
            obj = BookRepoObj;
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var books = await obj.GetAllBooksAsync();
            var dtos = new List<BookDto>();

            foreach (var b in books)
            {
                dtos.Add(new BookDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author,
                    AvailableCopies = b.AvailableCopies,
                    TotalCopies = b.TotalCopies
                });
            }
            return dtos;
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var b = await obj.GetBookByIdAsync(id);
            if (b == null)
            {
                return null;
            }

            return new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Author = b.Author,
                AvailableCopies = b.AvailableCopies,
                TotalCopies = b.TotalCopies
            };
        }

        public async Task<string> AddBookAsync(CreateBookDto b)
        {
            var domainBook = new Book
            {
                Title = b.Title,
                Author = b.Author,
                AvailableCopies = b.AvailableCopies,
                TotalCopies = b.TotalCopies
            };

            await obj.AddBookAsync(domainBook);
            return "book added successfully";
        }

        public async Task<bool> UpdateBookAsync(int id, BookDto b)
        {
            var exist = await obj.GetBookByIdAsync(id);
            if (exist == null)
            {
                return false;
            }

            var domainBook = new Book
            {
                Title = b.Title,
                Author = b.Author,
                AvailableCopies = b.AvailableCopies,
                TotalCopies = b.TotalCopies
            };

            await obj.UpdateBookAsync(id, domainBook);
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var exist = await obj.GetBookByIdAsync(id);
            if (exist == null)
            {
                return false;
            }

            return await obj.DeleteBookAsync(id);
        }
    }
}
