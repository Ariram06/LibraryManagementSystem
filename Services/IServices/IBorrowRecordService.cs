using LibraryManagementSystem.DTOs;

namespace LibraryManagementSystem.Services.IServices
{
    public interface IBorrowRecordService
    {
        Task<List<BorrowRecordDto>> GetAllRecordsAsync();
        Task<BorrowRecordDto> GetRecordByIdAsync(int id);
        Task<string> CreateBorrowRecordAsync(CreateBorrowRecordDto br);
        Task<bool> ReturnBookAsync(int id);
        Task<bool> DeleteRecordAsync(int id);
    }
}
