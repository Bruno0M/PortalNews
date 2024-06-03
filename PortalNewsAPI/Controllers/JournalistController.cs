using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalNews.Application.DTOs;
using PortalNews.Infrastructure.Interfaces;
using System.Security.Claims;

namespace PortalNewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalistController : ControllerBase
    {
        private readonly IJournalistRepository _journalistRepository;
        public JournalistController(IJournalistRepository journalistRepository)
        {
            _journalistRepository = journalistRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(JournalistDto journalistDTO)
        {
            var response = await _journalistRepository.Register(journalistDTO);

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(JournalistLoginDto journalistDTO)
        {
            var (response, token) = await _journalistRepository.Login(journalistDTO);

            return Ok(new { response, token });
        }

        [Authorize]
        [HttpGet("Me")]
        public async Task<IActionResult> GetDataJournalist()
        {
            var journalistId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _ = int.TryParse(journalistId, out int id);

            var response = await _journalistRepository.GetDataJournalist(id);

            return Ok(response);
        }
    }
}
