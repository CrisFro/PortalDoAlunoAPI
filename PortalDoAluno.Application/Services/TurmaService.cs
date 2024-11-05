using PortalDoAluno.Domain.Entities;
using PortalDoAluno.Domain.Interfaces;

namespace PortalDoAluno.Application.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<IEnumerable<Turma>> GetAllTurmasAsync()
        {
            return await _turmaRepository.GetAllTurmasAsync();
        }

        public async Task<Turma> GetTurmaByIdAsync(int id)
        {
            return await _turmaRepository.GetTurmaByIdAsync(id);
        }

        public async Task<Turma> CreateTurmaAsync(Turma turma)
        {
            turma.Id = await _turmaRepository.CreateTurmaAsync(turma);
            return turma;
        }

        public async Task UpdateTurmaAsync(Turma turma)
        {
            await _turmaRepository.UpdateTurmaAsync(turma);
        }

        public async Task DeleteTurmaAsync(int id)
        {
            await _turmaRepository.DeleteTurmaAsync(id);
        }
    }
}
