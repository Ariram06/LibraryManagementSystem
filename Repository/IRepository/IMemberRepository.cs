using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(int id);
        Task AddMemberAsync(Member m);
        Task UpdateMemberAsync(int id, Member m);
        Task<bool> DeleteMemberAsync(int id);
    }
}
