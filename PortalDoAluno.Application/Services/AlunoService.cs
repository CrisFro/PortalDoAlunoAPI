using PortalDoAluno.Domain.Entities;
using PortalDoAluno.Domain.Interfaces;

namespace PortalDoAluno.Application.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<IEnumerable<Aluno>> GetAllAlunosAsync()
        {
            return await _alunoRepository.GetAllAlunosAsync();
        }

        public async Task<Aluno> GetAlunoByIdAsync(int id)
        {
            return await _alunoRepository.GetAlunoByIdAsync(id);
        }

        public async Task<Aluno> CreateAsync(Aluno aluno)
        {
            if (aluno == null)
            {
                throw new ArgumentNullException(nameof(aluno), "O Aluno não pode ser nulo.");
            }

            int id = await _alunoRepository.CreateAlunoAsync(aluno);
            aluno.Id = id;
            return aluno;
        }

        public async Task UpdateAlunoAsync(Aluno aluno)
        {
            if (aluno == null)
            {
                throw new ArgumentNullException(nameof(aluno), "O Aluno não pode ser nulo.");
            }

            await _alunoRepository.UpdateAlunoAsync(aluno);
        }

        public async Task DeleteAlunoAsync(int id)
        {
            await _alunoRepository.DeleteAlunoAsync(id);
        }
    }
}
