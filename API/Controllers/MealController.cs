using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MealController : ControllerBase
    {
        private readonly MealService m_service;

        public MealController(MealService service)
        {
            m_service = service;
        }

        //--------------------------------------------------
        // GET all meals by WeekPlan
        //--------------------------------------------------
        [HttpGet("{weekPlanId}")]
        public async Task<IActionResult> GetAll(int weekPlanId)
        {
            var result = await m_service.GetAllByWeekPlanIdAsync(weekPlanId);
            return Ok(result);
        }

        //--------------------------------------------------
        // GET meal by id
        //--------------------------------------------------
        [HttpGet("item/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await m_service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //--------------------------------------------------
        // POST (FIXED)
        //--------------------------------------------------
        [HttpPost("{weekPlanId}")]
        public async Task<IActionResult> Create(int weekPlanId, [FromBody] Meal model)
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

        //--------------------------------------------------
        // PUT
        //--------------------------------------------------
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Meal model)
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

        //--------------------------------------------------
        // DELETE
        //--------------------------------------------------
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