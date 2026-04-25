using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutDayController : ControllerBase
    {
        private readonly WorkoutService m_workoutService;

        public WorkoutDayController(WorkoutService workoutService)
        {
            m_workoutService = workoutService;
        }

        //----------------------------------------------------------------
        // GET all days
        //----------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAllDays()
        {
            var days = await m_workoutService.GetAllDaysAsync();
            if (days != null)
                return Ok(days);
            return NotFound();
        }
        //----------------------------------------------------------------
        // GET day by id
        //----------------------------------------------------------------

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDayById(int id)
        {
            var dayId = await m_workoutService.GetDayByIdAsync(id);
            if (dayId != null)
                return Ok(dayId);

            return NotFound($"Day with Id [{id}] not found");
        }
        //----------------------------------------------------------------
        // add new Day
        //----------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> AddNewDay([FromBody] WorkoutDay workoutDay)
        {
            try
            {
                var newDay = await m_workoutService.addNewDayAsync(workoutDay);
                return CreatedAtAction(nameof(AddNewDay), new { id = newDay }, newDay);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //----------------------------------------------------------------
        // update Day
        //----------------------------------------------------------------
        [HttpPut]
        public async Task<IActionResult> UpdateDay([FromBody] WorkoutDay workoutDay)
        {
            try 
            {
                var day = await m_workoutService.UpdateDayAsync(workoutDay);
                    return Ok(day);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //----------------------------------------------------------------
        // delete Day
        //----------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDay(int id)
        {
            var result = await m_workoutService.DeleteDayAsync(id);
            if (!result)
                return NotFound($"Day with Id [{id}] not found");
            return Ok(result);
        }
    }
}
