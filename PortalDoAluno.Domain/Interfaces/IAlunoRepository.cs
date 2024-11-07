using PortalDoAluno.Domain.Entities;

namespace PortalDoAluno.Domain.Interfaces
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> GetAllAlunosAsync();
        Task<Aluno> GetAlunoByIdAsync(int id);
        Task<int> CreateAlunoAsync(Aluno aluno);
        Task UpdateAlunoAsync(Aluno aluno);
        Task DeleteAlunoAsync(int id);
        Task<AlunoTurma> GetRelacionamentoAsync(int alunoId, int turmaId);
        Task RelacionarAlunoNaTurmaAsync(int alunoId, int turmaId);
        Task<Aluno> GetByUsuarioAsync(string usuario);
    }
}
