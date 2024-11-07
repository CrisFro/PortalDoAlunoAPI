using PortalDoAluno.Application.DTOs;

namespace PortalDoAluno.Application.Services
{
    public interface IRelacionamentoService
    {
        Task<IEnumerable<TurmaComAlunosDto>> GetTurmasComAlunosAsync();
        Task AssociarAlunoTurmaAsync(int alunoId, int turmaId);
        Task<bool> VerificarAssociacaoExistenteAsync(int alunoId, int turmaId);
        Task DesativarAlunoTurmaAsync(int alunoId, int turmaId);
        Task UpdateRelacionamentoAsync(int alunoId, int turmaId);
    }
}
