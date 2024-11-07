using Microsoft.AspNetCore.Mvc;
using PortalDoAluno.Application.Services;

namespace PortalDoAlunoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacionamentosController : ControllerBase
    {
        private readonly IRelacionamentoService _relacionamentoService;

        public RelacionamentosController(IRelacionamentoService relacionamentoService)
        {
            _relacionamentoService = relacionamentoService;
        }

        [HttpPost("associar")]
        public async Task<IActionResult> AssociarAlunoTurma(int alunoId, int turmaId)
        {
            try
            {
                await _relacionamentoService.AssociarAlunoTurmaAsync(alunoId, turmaId);
                return Ok("Aluno relacionado à turma com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}"); 
            }
        }

        [HttpGet("turmas-com-alunos")]
        public async Task<IActionResult> GetTurmasComAlunos()
        {
            try
            {
                var turmasComAlunos = await _relacionamentoService.GetTurmasComAlunosAsync();
                return Ok(turmasComAlunos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        [HttpPost("atualizar")]
        public async Task<IActionResult> UpdateRelacionamento(int alunoId, int turmaId)
        {
            try
            {
                await _relacionamentoService.UpdateRelacionamentoAsync(alunoId, turmaId);
                return Ok("Relação atualizada com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        [HttpPost("desativar")]
        public async Task<IActionResult> DesativarAlunoTurma(int alunoId, int turmaId)
        {
            try
            {
                await _relacionamentoService.DesativarAlunoTurmaAsync(alunoId, turmaId);
                return Ok("Relação desativada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
