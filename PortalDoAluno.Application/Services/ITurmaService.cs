using PortalDoAluno.Application.DTOs;
using PortalDoAluno.Domain.Entities;

namespace PortalDoAluno.Application.Services
{
    public interface ITurmaService
    {
        Task<IEnumerable<TurmaDto>> GetAllTurmasAsync();
        Task<Turma> GetTurmaByIdAsync(int id);
        Task<TurmaDto> CreateTurmaAsync(TurmaDto turmaDto);
        Task UpdateTurmaAsync(TurmaDto turmaDto);
        Task DeleteTurmaAsync(int id);
    }
}
