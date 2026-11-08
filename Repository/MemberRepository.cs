using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Repository
{
    public class MemberRepository : IMemberRepository
    {
        protected readonly AppDbContext obj;

        public MemberRepository(AppDbContext AppDBObj)
        {
            obj = AppDBObj;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            return await obj.Members.ToListAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await obj.Members.FindAsync(id);
        }

        public async Task AddMemberAsync(Member m)
        {
            await obj.Members.AddAsync(m);
            await obj.SaveChangesAsync();
        }

        public async Task UpdateMemberAsync(int id, Member m)
        {
            var existingMember = await obj.Members.FindAsync(id);
            if (existingMember != null)
            {
                existingMember.Name = m.Name;
                existingMember.Email = m.Email;
                existingMember.PhoneNumber = m.PhoneNumber;
                await obj.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var m = await obj.Members.FindAsync(id);
            if (m != null)
            {
                obj.Members.Remove(m);
                await obj.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
