using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Repository
{
    public class BookRepository : IBookRepository
    {
        protected readonly AppDbContext obj;

        public BookRepository(AppDbContext AppDBObj)
        {
            obj = AppDBObj;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await obj.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await obj.Books.FindAsync(id);
        }

        public async Task AddBookAsync(Book b)
        {
            await obj.Books.AddAsync(b);
            await obj.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(int id, Book b)
        {
            var existingBook = await obj.Books.FindAsync(id);
            if (existingBook != null)
            {
                existingBook.Title = b.Title;
                existingBook.Author = b.Author;
                existingBook.AvailableCopies = b.AvailableCopies;
                existingBook.TotalCopies = b.TotalCopies;
                await obj.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var b = await obj.Books.FindAsync(id);
            if (b != null)
            {
                obj.Books.Remove(b);
                await obj.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
