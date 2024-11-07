using PortalDoAluno.Domain.Entities;

namespace PortalDoAluno.Domain.Interfaces
{
    public interface IAlunoTurmaRepository
    {
        Task RelacionarAlunoNaTurmaAsync(int alunoId, int turmaId);
        Task<IEnumerable<AlunoTurma>> ObterAlunosPorTurmaAsync(int turmaId);
        Task<int?> ObterTurmaAtualPorAlunoAsync(int alunoId);
        Task DesativarAlunoTurmaAsync(int alunoId, int turmaId);
        Task<bool> VerificarAssociacaoExistenteAsync(int alunoId, int turmaId);
        Task<bool> VerificarAssociacaoInativaAsync(int alunoId, int turmaId);
        Task ReativarAlunoNaTurmaAsync(int alunoId, int turmaId);
    }
}
