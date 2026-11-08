using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService obj;

        public MemberController(IMemberService IMemberServiceObj)
        {
            obj = IMemberServiceObj;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var members = await obj.GetAllMembersAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMemberById(int id)
        {
            var member = await obj.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound("Member not found");
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult> AddMember([FromBody] CreateMemberDto member)
        {
            if (member == null)
            {
                return BadRequest("Member data is null");
            }

            var result = await obj.AddMemberAsync(member);
            if (result != "member added successfully")
            {
                return BadRequest("failed to add member");
            }

            return Ok(new { message = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMember(int id, [FromBody] MemberDto member)
        {
            if (member == null)
            {
                return BadRequest("Member data is null");
            }

            if (id != member.MemberId)
            {
                return BadRequest("Member ID mismatch");
            }

            var result = await obj.UpdateMemberAsync(id, member);
            if (!result)
            {
                return NotFound("Member not found");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMember(int id)
        {
            var result = await obj.DeleteMemberAsync(id);
            if (!result)
            {
                return NotFound("Member not found");
            }
            return NoContent();
        }
    }
}
