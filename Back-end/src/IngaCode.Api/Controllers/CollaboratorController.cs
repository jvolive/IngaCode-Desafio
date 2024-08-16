using IngaCode.Application.DTOs.CollaboratorDTOs;
using IngaCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IngaCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService _collaboratorService;

        public CollaboratorController(ICollaboratorService collaboratorService)
        {
            _collaboratorService = collaboratorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollaboratorDto>>> GetAllCollaborators()
        {
            var collaborators = await _collaboratorService.GetAllCollaboratorsAsync();
            return Ok(collaborators);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CollaboratorDto>> GetCollaboratorById(Guid id)
        {
            var collaborator = await _collaboratorService.GetCollaboratorByIdAsync(id);
            if (collaborator == null)
            {
                return NotFound();
            }
            return Ok(collaborator);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<CollaboratorDto>> GetCollaboratorByName(string name)
        {
            var collaborator = await _collaboratorService.GetCollaboratorByNameAsync(name);
            if (collaborator == null)
            {
                return NotFound();
            }
            return Ok(collaborator);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollaborator([FromBody] CollaboratorCreateDto createDto)
        {
            var result = await _collaboratorService.CreateCollaboratorAsync(createDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to create collaborator.");
        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateCollaborator(Guid id, [FromBody] CollaboratorUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }

            await _collaboratorService.UpdateCollaboratorAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCollaborator(Guid id)
        {
            await _collaboratorService.DeleteCollaboratorAsync(id);
            return NoContent();
        }
    }
}
