using LibraryManagementSystem.Models;
using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services.IServices;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Services
{
    public class MemberService : IMemberService
    {
        protected readonly IMemberRepository obj;

        public MemberService(IMemberRepository MemberRepoObj)
        {
            obj = MemberRepoObj;
        }

        public async Task<List<MemberDto>> GetAllMembersAsync()
        {
            var members = await obj.GetAllMembersAsync();
            var dtos = new List<MemberDto>();

            foreach (var m in members)
            {
                dtos.Add(new MemberDto
                {
                    MemberId = m.MemberId,
                    Name = m.Name,
                    Email = m.Email,
                    PhoneNumber = m.PhoneNumber
                });
            }
            return dtos;
        }

        public async Task<MemberDto> GetMemberByIdAsync(int id)
        {
            var m = await obj.GetMemberByIdAsync(id);
            if (m == null)
            {
                return null;
            }

            return new MemberDto
            {
                MemberId = m.MemberId,
                Name = m.Name,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber
            };
        }

        public async Task<string> AddMemberAsync(CreateMemberDto m)
        {
            var domainMember = new Member
            {
                Name = m.Name,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber
            };

            await obj.AddMemberAsync(domainMember);
            return "member added successfully";
        }

        public async Task<bool> UpdateMemberAsync(int id, MemberDto m)
        {
            var exist = await obj.GetMemberByIdAsync(id);
            if (exist == null)
            {
                return false;
            }

            var domainMember = new Member
            {
                Name = m.Name,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber
            };

            await obj.UpdateMemberAsync(id, domainMember);
            return true;
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var exist = await obj.GetMemberByIdAsync(id);
            if (exist == null)
            {
                return false;
            }

            return await obj.DeleteMemberAsync(id);
        }
    }
}
