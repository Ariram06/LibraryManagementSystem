using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IFineRepository
    {
        Task<List<Fine>> GetAllFinesAsync();
        Task<Fine> GetFineByIdAsync(int id);
        Task<Fine> GetFineByBorrowRecordIdAsync(int borrowRecordId);
        Task AddFineAsync(Fine f);
        Task UpdateFineAsync(int id, Fine f);
        Task<bool> DeleteFineAsync(int id);
    }
}
