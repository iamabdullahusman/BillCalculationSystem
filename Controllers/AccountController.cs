using BillCalculation.Foundation.DTO;
using BillCalculation.Service.IServies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillCalculationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] AccountDTO accountDTO)
        {
            try
            {
                var userId = await _accountService.CreateUser(accountDTO);
                return Ok(new { Message = "User created successfully", UserId = userId });
            }
            catch (Exception ex)
            {
                // Instead of 500, return 400 BadRequest with custom message
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] loginDTO loginDTO)
        {
            try
            {
                var response = await _accountService.LoginAccount(loginDTO);
                if (response == null)
                    return Unauthorized(new { Message = "Invalid email or password" });



                return Ok(new
                {
                    Message = "Login successfully",
                    data = response.Token,
                    //User = new
                    //{

                    //    response.FirstName,
                    //    response.LastName,
                    //    response.Email

                    //}
                });
            }


            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
