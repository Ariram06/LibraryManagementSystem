using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Repository
{
    public class BorrowRecordRepository : IBorrowRecordRepository
    {
        protected readonly AppDbContext obj;

        public BorrowRecordRepository(AppDbContext AppDBObj)
        {
            obj = AppDBObj;
        }

        public async Task<List<BorrowRecord>> GetAllRecordsAsync()
        {
            return await obj.BorrowRecords.ToListAsync();
        }

        public async Task<BorrowRecord> GetRecordByIdAsync(int id)
        {
            return await obj.BorrowRecords.FindAsync(id);
        }

        public async Task AddRecordAsync(BorrowRecord br)
        {
            await obj.BorrowRecords.AddAsync(br);
            await obj.SaveChangesAsync();
        }

        public async Task UpdateRecordAsync(int id, BorrowRecord br)
        {
            var existingRecord = await obj.BorrowRecords.FindAsync(id);
            if (existingRecord != null)
            {
                existingRecord.BookId = br.BookId;
                existingRecord.MemberId = br.MemberId;
                existingRecord.BorrowDate = br.BorrowDate;
                existingRecord.DueDate = br.DueDate;
                existingRecord.ReturnDate = br.ReturnDate;
                existingRecord.Status = br.Status;
                await obj.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteRecordAsync(int id)
        {
            var br = await obj.BorrowRecords.FindAsync(id);
            if (br != null)
            {
                obj.BorrowRecords.Remove(br);
                await obj.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
