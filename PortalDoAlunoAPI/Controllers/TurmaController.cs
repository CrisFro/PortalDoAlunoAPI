using Microsoft.AspNetCore.Mvc;
using PortalDoAluno.Application.DTOs;
using PortalDoAluno.Application.Services;
using PortalDoAluno.Domain.Entities;

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
        public async Task<ActionResult<IEnumerable<Turma>>> GetAllTurmas()
        {
            var turmas = await _turmaService.GetAllTurmasAsync();
            return Ok(turmas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetTurmaById(int id)
        {
            var turma = await _turmaService.GetTurmaByIdAsync(id);
            if (turma == null)
                return NotFound();

            return Ok(turma);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTurma([FromBody] CreateTurmaDto createTurmaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var turma = new Turma
            {
                CursoId = (Curso)createTurmaDto.CursoId,
                Nome = createTurmaDto.Nome,
                Ano = createTurmaDto.Ano,
                IsActive = true
            };

            var turmaCriada = await _turmaService.CreateTurmaAsync(turma);
            return CreatedAtAction(nameof(GetTurmaById), new { id = turmaCriada.Id }, turmaCriada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTurma(int id, [FromBody] Turma turma)
        {
            if (id != turma.Id)
                return BadRequest("ID da turma não corresponde.");

            await _turmaService.UpdateTurmaAsync(turma);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            var turma = await _turmaService.GetTurmaByIdAsync(id);
            if (turma == null)
                return NotFound();

            await _turmaService.DeleteTurmaAsync(id);
            return NoContent();
        }
    }
}
