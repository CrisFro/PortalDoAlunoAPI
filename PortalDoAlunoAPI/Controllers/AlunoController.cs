﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAllAlunos()
        {
            var alunos = await _alunoService.GetAllAlunosAsync();
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAlunoById(int id)
        {
            var aluno = await _alunoService.GetAlunoByIdAsync(id);
            if (aluno == null)
                return NotFound();

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAluno([FromBody] Aluno aluno)
        {
            if (aluno == null)
            {
                return BadRequest("Dados do aluno estão nulos.");
            }

            var alunoCriado = await _alunoService.CreateAsync(aluno);
            return CreatedAtAction(nameof(GetAlunoById), new { id = alunoCriado.Id }, alunoCriado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAluno(int id, [FromBody] Aluno aluno)
        {
            if (id != aluno.Id)
                return BadRequest("ID do aluno não corresponde.");

            await _alunoService.UpdateAlunoAsync(aluno);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _alunoService.GetAlunoByIdAsync(id);
            if (aluno == null)
                return NotFound();

            await _alunoService.DeleteAlunoAsync(id);
            return NoContent();
        }
    }
}