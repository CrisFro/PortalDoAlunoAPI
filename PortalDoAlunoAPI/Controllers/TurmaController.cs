using Microsoft.AspNetCore.Mvc;
using PortalDoAluno.Application.DTOs;
using PortalDoAluno.Application.Services;

namespace PortalDoAlunoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaService _turmaService;

        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurmaDto>>> GetAllTurmas()
        {
            var turmas = await _turmaService.GetAllTurmasAsync();
            return Ok(turmas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TurmaDto>> GetTurmaById(int id)
        {
            var turma = await _turmaService.GetTurmaByIdAsync(id);
            if (turma == null)
                return NotFound("Turma não encontrada.");

            return Ok(turma);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTurma([FromBody] TurmaDto turmaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdTurma = await _turmaService.CreateTurmaAsync(turmaDto);
                return CreatedAtAction(nameof(GetTurmaById), new { id = createdTurma.Id }, createdTurma);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTurma(int id, [FromBody] TurmaDto turmaDto)
        {
            if (id != turmaDto.Id)
            {
                return BadRequest("O ID da turma no URL não corresponde ao ID no corpo da solicitação.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _turmaService.UpdateTurmaAsync(turmaDto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            try
            {
                await _turmaService.DeleteTurmaAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
