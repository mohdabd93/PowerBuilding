using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeekPlanController : ControllerBase
    {
        private readonly WeekPlanService m_service;

        public WeekPlanController(WeekPlanService service)
        {
            m_service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await m_service.GetAllWeekPlansAsync();
            return Ok(result);
        }
        [HttpGet("full")]
        [Authorize]
        public async Task<IActionResult> GetFullWeekPlan()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userId, out var guid))
                return Unauthorized();

            var result = await m_service.GetFullWeekPlanAsync(guid);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await m_service.GetWeekPlanByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WeekPlan model)
        {
            try
            {
                var result = await m_service.AddWeekPlanAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WeekPlan model)
        {
            try
            {
                var result = await m_service.UpdateWeekPlanAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await m_service.DeleteWeekPlanAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}