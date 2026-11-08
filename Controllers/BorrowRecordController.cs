using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowRecordController : ControllerBase
    {
        private readonly IBorrowRecordService obj;

        public BorrowRecordController(IBorrowRecordService IBorrowRecordServiceObj)
        {
            obj = IBorrowRecordServiceObj;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var records = await obj.GetAllRecordsAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRecordById(int id)
        {
            var record = await obj.GetRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound("Record not found");
            }
            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult> BorrowBook([FromBody] CreateBorrowRecordDto record)
        {
            if (record == null)
            {
                return BadRequest("Record data is null");
            }

            var result = await obj.CreateBorrowRecordAsync(record);
            if (result != "book added successfully")
            {
                return BadRequest(result);
            }

            return Ok(new { message = result });
        }

        [HttpPut("return/{id}")]
        public async Task<ActionResult> ReturnBook(int id)
        {
            var result = await obj.ReturnBookAsync(id);
            if (!result)
            {
                return BadRequest("Invalid record ID or book already returned.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecord(int id)
        {
            var result = await obj.DeleteRecordAsync(id);
            if (!result)
            {
                return NotFound("Record not found");
            }
            return NoContent();
        }
    }
}
