using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeekPlanController : ControllerBase
    {
        private readonly WeekPlanService m_weekPlanService;

        public WeekPlanController(WeekPlanService weekPlanService)
        {
            m_weekPlanService = weekPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWeekPlans()
        {
            var allWeeks = await m_weekPlanService.GetAllWeekPlansAsync();
            return Ok(allWeeks);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetWeekPlanById(int id)
        {
            var week = await m_weekPlanService.GetWeekPlanByIdAsync(id);
            if (week == null)
                return NotFound($"Week with Id [{id}] not found");
            return Ok(week);
        }
        [HttpPost]
        public async Task<IActionResult> AddWeekPlan([FromBody] WeekPlan weekPlan)
        {
            try
            {
                var newWeek = await m_weekPlanService.AddWeekPlanAsync(weekPlan);
                return CreatedAtAction(nameof(GetWeekPlanById), new { id = newWeek.Id }, newWeek);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWeekPlan([FromBody] WeekPlan weekPlan)
        {
            try
            {
                var updatedWeek = await m_weekPlanService.UpdateWeekPlanAsync(weekPlan);
                return Ok(updatedWeek);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeekPlan(int id)
        {
            var result = await m_weekPlanService.DeleteWeekPlanAsync(id);
            if (!result)
                return NotFound($"Week with Id [{id}] not found");
            return NoContent();
        }
    }
}
