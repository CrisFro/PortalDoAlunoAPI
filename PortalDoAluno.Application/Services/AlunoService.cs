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
            foreach (var aluno in alunos)
            {
                aluno.Senha = string.Empty;
            }
            return alunos;
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
            var existingUser = await _alunoRepository.GetByUsuarioAsync(aluno.Usuario);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Já existe um aluno com este usuário.");
            }

            aluno.Senha = HashSenha(aluno.Senha);
            aluno.Id = await _alunoRepository.CreateAlunoAsync(aluno);
            return aluno;
        }

        public async Task UpdateAlunoAsync(Aluno aluno)
        {
            var existingAluno = await _alunoRepository.GetAlunoByIdAsync(aluno.Id);
            if (existingAluno == null)
            {
                throw new InvalidOperationException("Aluno não encontrado.");
            }

            if (!string.IsNullOrWhiteSpace(aluno.Senha))
            {
                aluno.Senha = HashSenha(aluno.Senha);
            }
            else
            {
                aluno.Senha = existingAluno.Senha; 
            }

            await _alunoRepository.UpdateAlunoAsync(aluno);
        }

        public async Task DeleteAlunoAsync(int id)
        {
            var existingAluno = await _alunoRepository.GetAlunoByIdAsync(id);
            if (existingAluno == null)
            {
                throw new InvalidOperationException("Aluno não encontrado.");
            }

            await _alunoRepository.DeleteAlunoAsync(id);
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
    }
}
