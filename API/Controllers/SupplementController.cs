using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplementController : ControllerBase
    {
        private readonly SupplementService m_supplement;

        public SupplementController(SupplementService supplement)
        {
            m_supplement = supplement;
        }

        //--------------------------------------------------
        // GET api/supplement
        //--------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAllSupplementsAsync()
        {
            var allSupplements = await m_supplement.GetAllSupplementsAsync();
            if (allSupplements == null)
                return NoContent();
            return Ok(allSupplements);
        }

        //--------------------------------------------------
        // GET api/supplement/5
        //--------------------------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplementByIdAsync(int id)
        {
            var supplement = await m_supplement.GetSupplementByIdAsync(id);
            if (supplement == null)
                return NotFound($"Supplement with Id [{id}] not found");
            return Ok(supplement);
        }

        //--------------------------------------------------
        // POST api/supplement
        //--------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> AddNewSupplement([FromBody] Supplement newSupplement)
        {
            try
            {
                var newSup = await m_supplement.AddSupplementAsync(newSupplement);
                return CreatedAtAction(nameof(GetSupplementByIdAsync), new { id = newSup.Id }, newSup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //--------------------------------------------------
        // PUT api/supplement
        //--------------------------------------------------
        [HttpPut]
        public async Task<IActionResult> UpdateSupplement([FromBody] Supplement updatedSupplement)
        {
            try
            {
                var updated = await m_supplement.UpdateSupplementAsync(updatedSupplement);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //--------------------------------------------------
        // DELETE api/supplement/5
        //--------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplement(int id)
        {
            var result = await m_supplement.DeleteSupplemet(id);
            if (!result)
                return NotFound($"Supplement with Id [{id}] not found");
            return NoContent();
        }
    }
}
