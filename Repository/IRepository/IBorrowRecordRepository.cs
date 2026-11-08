using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IBorrowRecordRepository
    {
        Task<List<BorrowRecord>> GetAllRecordsAsync();
        Task<BorrowRecord> GetRecordByIdAsync(int id);
        Task AddRecordAsync(BorrowRecord br);
        Task UpdateRecordAsync(int id, BorrowRecord br);
        Task<bool> DeleteRecordAsync(int id);
    }
}
