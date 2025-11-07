using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _service;
        public ExpensesController(IExpenseService service) => _service = service;

        private string GetUserId() => User.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User id missing");

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var res = await _service.GetAllForUserAsync(userId);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = GetUserId();
            var res = await _service.GetByIdForUserAsync(userId, id);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseDto dto)
        {
            var userId = GetUserId();
            var created = await _service.AddExpenseForUserAsync(userId, dto);
            return Ok(created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ExpenseDto dto)
        {
            var userId = GetUserId();
            await _service.UpdateExpenseForUserAsync(userId, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            await _service.DeleteExpenseForUserAsync(userId, id);
            return Ok();
        }
    }
}
