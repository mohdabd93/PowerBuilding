using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutDayController : ControllerBase
    {
        private readonly WorkoutService m_service;

        public WorkoutDayController(WorkoutService service)
        {
            m_service = service;
        }

        [HttpGet("{weekPlanId}")]
        public async Task<IActionResult> GetAll(int weekPlanId)
        {
            var result = await m_service.GetAllByWeekPlanIdAsync(weekPlanId);
            return Ok(result);
        }

        [HttpGet("day/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await m_service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("{weekPlanId}")]
        public async Task<IActionResult> Create(int weekPlanId, [FromBody] WorkoutDay model)
        {
            try
            {
                var result = await m_service.AddAsync(weekPlanId, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WorkoutDay model)
        {
            try
            {
                var result = await m_service.UpdateAsync(model);
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
            var result = await m_service.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}