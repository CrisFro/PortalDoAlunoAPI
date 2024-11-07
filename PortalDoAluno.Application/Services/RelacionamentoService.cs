using PortalDoAluno.Application.DTOs;
using PortalDoAluno.Domain.Interfaces;

namespace PortalDoAluno.Application.Services
{
    public class RelacionamentoService : IRelacionamentoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;

        public RelacionamentoService(
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IAlunoTurmaRepository alunoTurmaRepository)
        {
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _alunoTurmaRepository = alunoTurmaRepository;
        }

        public async Task<IEnumerable<TurmaComAlunosDto>> GetTurmasComAlunosAsync()
        {
            var turmas = await _turmaRepository.GetAllTurmasAsync();
            var alunos = await _alunoRepository.GetAllAlunosAsync();
            var turmasComAlunos = new List<TurmaComAlunosDto>();

            foreach (var turma in turmas)
            {
                var relacionamentos = await _alunoTurmaRepository.ObterAlunosPorTurmaAsync(turma.Id);
                var alunosRelacionados = alunos
                    .Where(aluno => relacionamentos.Any(r => r.AlunoId == aluno.Id))
                    .Select(a => new AlunoDto { Id = a.Id, Nome = a.Nome, Usuario = a.Usuario })
                    .ToList();

                turmasComAlunos.Add(new TurmaComAlunosDto
                {
                    TurmaId = turma.Id,
                    TurmaNome = turma.Nome,
                    Alunos = alunosRelacionados
                });
            }

            return turmasComAlunos;
        }

        public async Task AssociarAlunoTurmaAsync(int alunoId, int turmaId)
        {
            var existeAssociacaoAtiva = await _alunoTurmaRepository.VerificarAssociacaoExistenteAsync(alunoId, turmaId);
            var existeAssociacaoInativa = await _alunoTurmaRepository.VerificarAssociacaoInativaAsync(alunoId, turmaId);

            if (existeAssociacaoAtiva)
            {
                throw new InvalidOperationException("O aluno já está associado a esta turma.");
            }
            else if (existeAssociacaoInativa)
            {
                await _alunoTurmaRepository.ReativarAlunoNaTurmaAsync(alunoId, turmaId);
            }
            else
            {
                await _alunoTurmaRepository.RelacionarAlunoNaTurmaAsync(alunoId, turmaId);
            }
        }

        public async Task<bool> VerificarAssociacaoExistenteAsync(int alunoId, int turmaId)
        {
            return await _alunoTurmaRepository.VerificarAssociacaoExistenteAsync(alunoId, turmaId);
        }

        public async Task DesativarAlunoTurmaAsync(int alunoId, int turmaId)
        {
            await _alunoTurmaRepository.DesativarAlunoTurmaAsync(alunoId, turmaId);
        }

        public async Task UpdateRelacionamentoAsync(int alunoId, int novaTurmaId)
        {
            var existeAssociacaoAtiva = await _alunoTurmaRepository.VerificarAssociacaoExistenteAsync(alunoId, novaTurmaId);
            if (existeAssociacaoAtiva)
            {
                throw new InvalidOperationException("O aluno já está associado a esta turma.");
            }

            var existeAssociacaoInativa = await _alunoTurmaRepository.VerificarAssociacaoInativaAsync(alunoId, novaTurmaId);
            if (existeAssociacaoInativa)
            {
                await _alunoTurmaRepository.ReativarAlunoNaTurmaAsync(alunoId, novaTurmaId);
            }
            else
            {
                await _alunoTurmaRepository.RelacionarAlunoNaTurmaAsync(alunoId, novaTurmaId);
            }

            var turmaAtualId = await _alunoTurmaRepository.ObterTurmaAtualPorAlunoAsync(alunoId);

            if (turmaAtualId.HasValue && turmaAtualId.Value != novaTurmaId)
            {
                await _alunoTurmaRepository.DesativarAlunoTurmaAsync(alunoId, turmaAtualId.Value);
            }
        }


    }
}

