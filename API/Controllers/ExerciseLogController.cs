using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseLogController : ControllerBase
    {
        private readonly ExerciseLogService m_exerciseLogService;

        public ExerciseLogController(ExerciseLogService exerciseLogService)
        {
            m_exerciseLogService = exerciseLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allLogs = await m_exerciseLogService.GetExerciseLogsAsync();
            return Ok(allLogs);
        }

        // 🔹 get single log
        [HttpGet("log/{id}")]
        public async Task<IActionResult> GetLogById(int id)
        {
            var log = await m_exerciseLogService.GetExerciseLogByIdAsync(id);

            if (log == null)
                return NotFound();

            return Ok(log);
        }

        // last 2 logs for exercise
        [HttpGet("exercise/{exerciseId}")]
        public async Task<IActionResult> GetLogsByExercise(int exerciseId)
        {
            var filtered = await m_exerciseLogService.GetLastLogsByExerciseIdAsync(exerciseId);

            return Ok(filtered); 
        }

        [HttpPost]
        public async Task<IActionResult> AddNewLog([FromBody] ExerciseLog exerciseLog)
        {
            var result = await m_exerciseLogService.AddExerciseAsync(exerciseLog);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLog([FromBody] ExerciseLog exerciseLog)
        {
            var result = await m_exerciseLogService.UpdateExerciseAsync(exerciseLog);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await m_exerciseLogService.RemoveExerciseAsync(id);
            return Ok(result);
        }

    }
}
