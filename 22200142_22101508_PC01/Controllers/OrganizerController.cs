using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PC1.DOMAIN.CORE.Entities;
using PC1.DOMAIN.CORE.Interfaces;

namespace _22200142_22101508_PC01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {

        private readonly IOrganizersRepository _organizerRepository;

        public OrganizerController(IOrganizersRepository organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }
        [HttpGet()]
        public async Task<IActionResult> GetOrganizer()
        {
            var organizers = await _organizerRepository.GetOrganizers();
            return Ok(organizers);
        }

        [HttpPost()]
        public async Task<IActionResult> InsertOrganizer([FromBody] Organizers organizer)
        {
            if (organizer == null)
            {
                return BadRequest("El organizador no puede ser nulo.");
            }

            var result = await _organizerRepository.Insert(organizer);

            if (result > 0) // Suponiendo que devuelve el ID del organizador insertado o un número positivo en caso de éxito
            {
                return CreatedAtAction(nameof(GetOrganizer), new { id = result }, organizer);
            }

            return StatusCode(500, "Error al insertar el organizador.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _organizerRepository.Delete(id);
            if (!result) return NotFound();
            return Ok(result);
        }

    }
}
