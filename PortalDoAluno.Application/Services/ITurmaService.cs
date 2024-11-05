using PortalDoAluno.Domain.Entities;

namespace PortalDoAluno.Application.Services
{
    public interface ITurmaService
    {
        Task<IEnumerable<Turma>> GetAllTurmasAsync();
        Task<Turma> GetTurmaByIdAsync(int id);
        Task<Turma> CreateTurmaAsync(Turma turma);
        Task UpdateTurmaAsync(Turma turma);
        Task DeleteTurmaAsync(int id);
    }
}
