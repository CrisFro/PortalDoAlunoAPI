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
                return NotFound();

            return Ok(turma);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTurma([FromBody] TurmaDto turmaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var turmaCriada = await _turmaService.CreateTurmaAsync(turmaDto);

            return CreatedAtAction(nameof(GetTurmaById), new { id = turmaCriada.Id }, turmaCriada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTurma(int id, [FromBody] TurmaDto turmaDto)
        {
            if (id != turmaDto.Id)
                return BadRequest("O ID da turma no URL não corresponde ao ID no corpo da solicitação.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var turmaExistente = await _turmaService.GetTurmaByIdAsync(id);
            if (turmaExistente == null)
            {
                return NotFound($"Turma com ID {id} não encontrada.");
            }

            await _turmaService.UpdateTurmaAsync(turmaDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            var turma = await _turmaService.GetTurmaByIdAsync(id);
            if (turma == null)
            {
                return NotFound($"Turma com ID {id} não encontrada.");
            }

            turma.IsActive = false;

            var turmaDto = new TurmaDto
            {
                Id = turma.Id,
                CursoId = turma.CursoId,
                Nome = turma.Nome,
                Ano = turma.Ano,
                IsActive = turma.IsActive
            };

            await _turmaService.UpdateTurmaAsync(turmaDto);

            return NoContent();
        }
    }
}
