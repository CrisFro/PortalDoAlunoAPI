using Microsoft.AspNetCore.Mvc;
using PortalDoAluno.Application.DTOs;
using PortalDoAluno.Application.Services;
using PortalDoAluno.Domain.Entities;

namespace PortalDoAlunoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDto>>> GetAllAlunos()
        {
            var alunos = await _alunoService.GetAllAlunosAsync();
            var alunoDtos = alunos.Select(a => new AlunoDto
            {
                Id = a.Id,
                Nome = a.Nome,
                Usuario = a.Usuario,
                IsActive = a.IsActive
            }).ToList();

            return Ok(alunoDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoDto>> GetAlunoById(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAlunoByIdAsync(id);
                var alunoDto = new AlunoDto
                {
                    Id = aluno.Id,
                    Nome = aluno.Nome,
                    Usuario = aluno.Usuario,
                    IsActive = aluno.IsActive
                };

                return Ok(alunoDto);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAluno([FromBody] AlunoDto alunoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aluno = new Aluno
            {
                Nome = alunoDto.Nome,
                Usuario = alunoDto.Usuario,
                Senha = alunoDto.Senha,
                IsActive = alunoDto.IsActive
            };

            try
            {
                var alunoCriado = await _alunoService.CreateAsync(aluno);
                alunoDto.Id = alunoCriado.Id;
                return CreatedAtAction(nameof(GetAlunoById), new { id = alunoCriado.Id }, alunoDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAluno(int id, [FromBody] AlunoDto alunoDto)
        {
            if (id != alunoDto.Id)
            {
                return BadRequest("ID do aluno não corresponde.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aluno = new Aluno
            {
                Id = id,
                Nome = alunoDto.Nome,
                Usuario = alunoDto.Usuario,
                Senha = alunoDto.Senha,
                IsActive = alunoDto.IsActive
            };

            try
            {
                await _alunoService.UpdateAlunoAsync(aluno);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            try
            {
                await _alunoService.DeleteAlunoAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
