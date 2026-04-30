using API.Services;
using Domain.Entities;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseTemplateController : ControllerBase
    {
        private readonly ExerciseTemplateService m_service;

        public ExerciseTemplateController(ExerciseTemplateService service)
        {
            m_service = service;
        }

        //--------------------------------------------------
        // GET ALL
        //--------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await m_service.GetAllAsync();
            return Ok(data);
        }

        //--------------------------------------------------
        // GET FILTERED
        //--------------------------------------------------
        [HttpGet("filtered/{muscle}/{type}")]
        public async Task<IActionResult> GetFiltered(TargetMuscle muscle, ExerciseType type)
        {
            var data = await m_service.GetFilteredAsync(muscle, type);
            return Ok(data);
        }

        //--------------------------------------------------
        // GET BY MUSCLE
        //--------------------------------------------------
        [HttpGet("muscle/{muscle}")]
        public async Task<IActionResult> GetByMuscle(TargetMuscle muscle)
        {
            var data = await m_service.GetByMuscleAsync(muscle);
            return Ok(data);
        }

        //--------------------------------------------------
        // ADD
        //--------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExerciseTemplate model)
        {
            if (model == null)
                return BadRequest("Model is null");

            var result = await m_service.AddAsync(model);
            return Ok(result);
        }

        //--------------------------------------------------
        // UPDATE
        //--------------------------------------------------
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ExerciseTemplate model)
        {
            var result = await m_service.UpdateAsync(model);
            return Ok(result);
        }

        //--------------------------------------------------
        // DELETE
        //--------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await m_service.DeleteAsync(id);
            return Ok(result);
        }
    }
}