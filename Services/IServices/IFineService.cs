using LibraryManagementSystem.DTOs;

namespace LibraryManagementSystem.Services.IServices
{
    public interface IFineService
    {
        Task<List<FineDto>> GetAllFinesAsync();
        Task<FineDto> GetFineByIdAsync(int id);
        Task<string> CreateFineAsync(CreateFineDto f);
        Task<bool> PayFineAsync(int id, PayFineDto p);
        Task<bool> DeleteFineAsync(int id);
    }
}
