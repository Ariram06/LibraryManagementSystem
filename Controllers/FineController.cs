using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : ControllerBase
    {
        private readonly IFineService obj;

        public FineController(IFineService IFineServiceObj)
        {
            obj = IFineServiceObj;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var fines = await obj.GetAllFinesAsync();
            return Ok(fines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetFineById(int id)
        {
            var fine = await obj.GetFineByIdAsync(id);
            if (fine == null)
            {
                return NotFound("Fine not found");
            }
            return Ok(fine);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFine([FromBody] CreateFineDto fine)
        {
            if (fine == null)
            {
                return BadRequest("Fine data is null");
            }

            var result = await obj.CreateFineAsync(fine);
            if (result != "fine added successfully")
            {
                return BadRequest(result);
            }

            return Ok(new { message = result });
        }

        [HttpPut("pay/{id}")]
        public async Task<ActionResult> PayFine(int id, [FromBody] PayFineDto payment)
        {
            if (payment == null || payment.AmountToPay <= 0)
            {
                return BadRequest("Invalid payment amount");
            }

            var result = await obj.PayFineAsync(id, payment);
            if (!result)
            {
                return BadRequest("Fine not found or already fully paid");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFine(int id)
        {
            var result = await obj.DeleteFineAsync(id);
            if (!result)
            {
                return NotFound("Fine not found");
            }
            return NoContent();
        }
    }
}
