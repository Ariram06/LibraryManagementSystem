using LibraryManagementSystem.DTOs;

namespace LibraryManagementSystem.Services.IServices
{
    public interface IMemberService
    {
        Task<List<MemberDto>> GetAllMembersAsync();
        Task<MemberDto> GetMemberByIdAsync(int id);
        Task<string> AddMemberAsync(CreateMemberDto m);
        Task<bool> UpdateMemberAsync(int id, MemberDto m);
        Task<bool> DeleteMemberAsync(int id);
    }
}
