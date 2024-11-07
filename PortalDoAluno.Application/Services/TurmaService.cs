using PortalDoAluno.Application.DTOs;
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

        public async Task<IEnumerable<TurmaDto>> GetAllTurmasAsync()
        {
            var turmas = await _turmaRepository.GetAllTurmasAsync();
            return turmas.Select(t => new TurmaDto
            {
                Id = t.Id,
                CursoId = t.CursoId,
                Nome = t.Nome,
                Ano = t.Ano,
                IsActive = t.IsActive
            }).ToList();
        }

        public async Task<Turma> GetTurmaByIdAsync(int id)
        {
            return await _turmaRepository.GetTurmaByIdAsync(id);
        }

        public async Task<TurmaDto> CreateTurmaAsync(TurmaDto turmaDto)
        {
            var existeTurma = await _turmaRepository.GetTurmaByNomeAsync(turmaDto.Nome);
            if (existeTurma != null)
            {
                throw new InvalidOperationException("Já existe uma turma com este nome.");
            }

            var turma = new Turma
            {
                CursoId = turmaDto.CursoId,
                Nome = turmaDto.Nome,
                Ano = turmaDto.Ano,
                IsActive = turmaDto.IsActive
            };

            turma.Id = await _turmaRepository.CreateTurmaAsync(turma);
            turmaDto.Id = turma.Id;

            return turmaDto;
        }

        public async Task UpdateTurmaAsync(TurmaDto turmaDto)
        {
            var turma = new Turma
            {
                Id = turmaDto.Id ?? 0,
                CursoId = turmaDto.CursoId,
                Nome = turmaDto.Nome,
                Ano = turmaDto.Ano,
                IsActive = turmaDto.IsActive
            };

            await _turmaRepository.UpdateTurmaAsync(turma);
        }

        public async Task DeleteTurmaAsync(int id)
        {
            await _turmaRepository.DeleteTurmaAsync(id);
        }
    }
}
