using PortalDoAluno.Domain.Entities;

namespace PortalDoAluno.Domain.Interfaces
{
    public interface ITurmaRepository
    {
        Task<IEnumerable<Turma>> GetAllTurmasAsync();
        Task<Turma> GetTurmaByIdAsync(int id);
        Task<int> CreateTurmaAsync(Turma turma);
        Task UpdateTurmaAsync(Turma turma);
        Task DeleteTurmaAsync(int id);
    }
}
