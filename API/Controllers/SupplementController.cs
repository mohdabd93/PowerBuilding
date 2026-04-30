using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SupplementController : ControllerBase
    {
        private readonly SupplementService m_service;

        public SupplementController(SupplementService service)
        {
            m_service = service;
        }

        [HttpGet("{weekPlanId}")]
        public async Task<IActionResult> GetAll(int weekPlanId)
        {
            var result = await m_service.GetAllByWeekPlanIdAsync(weekPlanId);
            return Ok(result);
        }

        [HttpGet("item/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await m_service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("{weekPlanId}")]
        public async Task<IActionResult> Create(int weekPlanId, [FromBody] Supplement model)
        {
            var result = await m_service.AddAsync(weekPlanId, model);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Supplement model)
        {
            var result = await m_service.UpdateAsync(model);
            return Ok(result);
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