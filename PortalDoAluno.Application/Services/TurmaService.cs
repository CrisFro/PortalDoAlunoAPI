using PortalDoAluno.Application.DTOs;
using PortalDoAluno.Domain.Entities;
using PortalDoAluno.Domain.Extensions;
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
                IsActive = t.IsActive,
                CursoNome = t.CursoId != null ? ((Curso)t.CursoId).GetDescription() : string.Empty
            }).ToList();
        }

        public async Task<Turma> GetTurmaByIdAsync(int id)
        {
            return await _turmaRepository.GetTurmaByIdAsync(id);
        }

        public async Task<TurmaDto> CreateTurmaAsync(TurmaDto turmaDto)
        {
            var existingTurma = await _turmaRepository.GetTurmaByNomeECursoAsync(turmaDto.Nome, turmaDto.CursoId);

            if (existingTurma != null)
            {
                if (!existingTurma.IsActive)
                {
                    existingTurma.IsActive = true;
                    existingTurma.Ano = turmaDto.Ano; 
                    await _turmaRepository.UpdateTurmaAsync(existingTurma); 
                    turmaDto.Id = existingTurma.Id; 
                    return turmaDto;
                }

                throw new InvalidOperationException("Já existe uma turma ativa com este nome e curso.");
            }

            var newTurma = new Turma
            {
                CursoId = turmaDto.CursoId,
                Nome = turmaDto.Nome,
                Ano = turmaDto.Ano,
                IsActive = true
            };

            newTurma.Id = await _turmaRepository.CreateTurmaAsync(newTurma);
            turmaDto.Id = newTurma.Id;

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
            var turma = await _turmaRepository.GetTurmaByIdAsync(id);
            if (turma == null)
            {
                throw new InvalidOperationException("Turma não encontrada.");
            }

            turma.IsActive = false;
            await _turmaRepository.UpdateTurmaAsync(turma);
        }
    }
}

