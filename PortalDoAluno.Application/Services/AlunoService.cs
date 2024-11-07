using PortalDoAluno.Domain.Entities;
using PortalDoAluno.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

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
            var alunos = await _alunoRepository.GetAllAlunosAsync();
            return alunos.Select(aluno =>
            {
                aluno.Senha = string.Empty;
                return aluno;
            });
        }

        public async Task<Aluno> GetAlunoByIdAsync(int id)
        {
            var aluno = await _alunoRepository.GetAlunoByIdAsync(id);
            if (aluno == null)
            {
                throw new InvalidOperationException("Aluno não encontrado");
            }
            aluno.Senha = string.Empty; 
            return aluno;
        }

        public async Task<Aluno> CreateAsync(Aluno aluno)
        {
            var usuarioExistente = await _alunoRepository.GetByUsuarioAsync(aluno.Usuario);
            if (usuarioExistente != null)
            {
                throw new InvalidOperationException("Já existe um aluno com este usuário.");
            }

            aluno.Senha = HashSenha(aluno.Senha);
            aluno.Id = await _alunoRepository.CreateAlunoAsync(aluno);
            return aluno;
        }

        private string HashSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public async Task UpdateAlunoAsync(Aluno aluno)
        {
            if (!string.IsNullOrWhiteSpace(aluno.Senha))
            {
                aluno.Senha = HashSenha(aluno.Senha);
            }
            else
            {
                var alunoExistente = await _alunoRepository.GetAlunoByIdAsync(aluno.Id);
                if (alunoExistente != null)
                {
                    aluno.Senha = alunoExistente.Senha;
                }
            }

            await _alunoRepository.UpdateAlunoAsync(aluno);
        }

        public async Task DeleteAlunoAsync(int id)
        {
            await _alunoRepository.DeleteAlunoAsync(id);
        }

        public async Task RelacionarAlunoNaTurmaAsync(int alunoId, int turmaId)
        {
            var existeRelacionamento = await _alunoRepository.GetRelacionamentoAsync(alunoId, turmaId);
            if (existeRelacionamento != null)
            {
                throw new InvalidOperationException("Este aluno já está relacionado a esta turma.");
            }

            await _alunoRepository.RelacionarAlunoNaTurmaAsync(alunoId, turmaId);
        }
    }
}
