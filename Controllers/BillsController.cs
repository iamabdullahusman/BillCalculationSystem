using System.Security.Claims;
using BillCalculation.Foundation.DTO;
using BillCalculation.Service.IServies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillCalculationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillsController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillsController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bills = await _billService.GetAllAsync();                                   
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bill = await _billService.GetByIdAsync(id);
            if (bill == null) return NotFound();
            return Ok(bill);
        }
        //[HttpPost]
        //public async Task<IActionResult> Createuser([FromBody] BillCreateDTO bill)
        //{
        //    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    bill.UserId = userId;
        //    var id = await _billService.AddAsync(bill);
        //    return Ok(new { Id = id, Message = "Bill created successfully" });
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BillCreateDTO bill)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            bill.UserId = userId;
            var id = await _billService.AddAsync(bill);
            return Ok(new { Id = id, Message = "Bill created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BillDTO bill)
        {
            bill.ID = id;
            await _billService.UpdateAsync(bill);
            return Ok("Bill updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _billService.DeleteAsync(id);
            return Ok("Bill deleted successfully");
        }
    }
}
