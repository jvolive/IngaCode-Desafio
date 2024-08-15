using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IngaCode.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService _collaboratorService;

        public CollaboratorController(ICollaboratorService collaboratorService)
        {
            _collaboratorService = collaboratorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var collaborators = await _collaboratorService.GetAllCollaboratorsAsync();
            return Ok(collaborators);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var collaborator = await _collaboratorService.GetCollaboratorByIdAsync(id);

            if (collaborator == null)
                return NotFound();

            return Ok(collaborator);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Collaborator collaborator)
        {
            var createdCollaborator = await _collaboratorService.CreateCollaboratorAsync(collaborator);
            return CreatedAtAction(nameof(GetById), new { id = createdCollaborator.Id }, createdCollaborator);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Collaborator collaborator)
        {
            if (id != collaborator.Id)
                return BadRequest();

            await _collaboratorService.UpdateCollaboratorAsync(collaborator);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _collaboratorService.DeleteCollaboratorAsync(id);
            return NoContent();
        }
    }
}
