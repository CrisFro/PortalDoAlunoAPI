using PortalDoAluno.Domain.Entities;

namespace PortalDoAluno.Application.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> GetAllAlunosAsync();
        Task<Aluno> GetAlunoByIdAsync(int id);
        Task<Aluno> CreateAsync(Aluno aluno);
        Task UpdateAlunoAsync(Aluno aluno);
        Task DeleteAlunoAsync(int id);
    }
}
