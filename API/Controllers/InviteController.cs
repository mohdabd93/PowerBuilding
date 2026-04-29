using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/invite")]
    public class InviteController : ControllerBase
    {
        private readonly InviteService m_service;

        public InviteController(InviteService service)
        {
            m_service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string email)
        {
            var invite = await m_service.CreateInviteAsync(email);
            return Ok(invite);
        }
    }
}
