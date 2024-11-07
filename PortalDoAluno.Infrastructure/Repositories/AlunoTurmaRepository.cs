using Dapper;
using Microsoft.Extensions.Configuration;
using PortalDoAluno.Domain.Entities;
using PortalDoAluno.Domain.Interfaces;
using System.Data.SqlClient;

namespace PortalDoAluno.Infrastructure.Repositories
{
    public class AlunoTurmaRepository : IAlunoTurmaRepository
    {
        private readonly string _connectionString;

        public AlunoTurmaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PortalDoAlunoDB");
        }

        public async Task RelacionarAlunoNaTurmaAsync(int alunoId, int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO aluno_turma (aluno_id, turma_id) VALUES (@AlunoId, @TurmaId)";
            await connection.ExecuteAsync(sql, new { AlunoId = alunoId, TurmaId = turmaId });
        }

        public async Task<IEnumerable<AlunoTurma>> ObterAlunosPorTurmaAsync(int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT aluno_id AS AlunoId, turma_id AS TurmaId FROM aluno_turma WHERE turma_id = @TurmaId AND IsActive = 1;";
            return await connection.QueryAsync<AlunoTurma>(sql, new { TurmaId = turmaId });
        }

        public async Task<int?> ObterTurmaAtualPorAlunoAsync(int alunoId)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT turma_id FROM aluno_turma WHERE aluno_id = @AlunoId AND IsActive = 1;";
            return await connection.ExecuteScalarAsync<int?>(sql, new { AlunoId = alunoId });
        }

        public async Task DesativarAlunoTurmaAsync(int alunoId, int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE aluno_turma SET IsActive = 0 WHERE aluno_id = @AlunoId AND turma_id = @TurmaId";
            await connection.ExecuteAsync(sql, new { AlunoId = alunoId, TurmaId = turmaId });
        }

        public async Task<bool> VerificarAssociacaoExistenteAsync(int alunoId, int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT COUNT(1) FROM aluno_turma WHERE aluno_id = @AlunoId AND turma_id = @TurmaId AND IsActive = 1";
            var count = await connection.ExecuteScalarAsync<int>(sql, new { AlunoId = alunoId, TurmaId = turmaId });
            return count > 0;
        }

        public async Task<bool> VerificarAssociacaoInativaAsync(int alunoId, int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT COUNT(1) FROM aluno_turma WHERE aluno_id = @AlunoId AND turma_id = @TurmaId AND IsActive = 0";
            var count = await connection.ExecuteScalarAsync<int>(sql, new { AlunoId = alunoId, TurmaId = turmaId });
            return count > 0; 
        }

        public async Task ReativarAlunoNaTurmaAsync(int alunoId, int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE aluno_turma SET IsActive = 1 WHERE aluno_id = @AlunoId AND turma_id = @TurmaId";
            await connection.ExecuteAsync(sql, new { AlunoId = alunoId, TurmaId = turmaId });
        }
    }
}
