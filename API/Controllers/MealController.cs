using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly MealService m_mealService;

        public MealController(MealService mealService)
        {
            m_mealService = mealService;
        }

        //--------------------------------------------------
        // GET api/meal
        //--------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAllMeals()
        {
            var meals = await m_mealService.GetAllMealsAsync();
            if (meals != null)
                return Ok(meals);
            return NoContent();
        }

        //--------------------------------------------------
        // GET api/meal/5
        //--------------------------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMealById(int id)
        {
            var meal = await m_mealService.GetMealByIdAsync(id);
            if (meal != null)
                return Ok(meal);
            return NotFound($"Meal with Id [{id}] not found");
        }

        //--------------------------------------------------
        // GET api/meal/name/فطور
        //--------------------------------------------------
        [HttpGet("name/{mealName}")]
        public async Task<IActionResult> GetMealByName(string mealName)
        {
            var meal = await m_mealService.GetMealByNamAsync(mealName);
            if (meal != null)
                return Ok(meal);
            return NotFound($"Meal [{mealName}] not found");
        }

        //--------------------------------------------------
        // POST api/meal
        //--------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> AddNewMeal([FromBody] Meal meal)
        {
            try
            {
                var newMeal = await m_mealService.AddMealAsync(meal);
                return CreatedAtAction(nameof(GetMealById), new { id = newMeal.Id }, newMeal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //--------------------------------------------------
        // PUT api/meal
        //--------------------------------------------------
        [HttpPut]
        public async Task<IActionResult> UpdateMeal([FromBody] Meal meal)
        {
            try
            {
                var updatedMeal = await m_mealService.UpdateMealAsync(meal);
                return Ok(updatedMeal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //--------------------------------------------------
        // DELETE api/meal/5
        //--------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMeal(int id)
        {
            var result = await m_mealService.DeleteMealAsync(id);
            if (!result)
                return NotFound($"Meal with Id [{id}] not found");
            return NoContent();
        }
    }
}
