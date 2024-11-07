using PortalDoAluno.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using PortalDoAluno.Domain.Interfaces;

namespace PortalDoAluno.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly string _connectionString;

        public AlunoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PortalDoAlunoDB");
        }

        public async Task<IEnumerable<Aluno>> GetAllAlunosAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Aluno>("SELECT * FROM aluno WHERE IsActive = 1");
        }

        public async Task<Aluno> GetAlunoByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Aluno>("SELECT * FROM aluno WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> CreateAlunoAsync(Aluno aluno)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"
                INSERT INTO aluno (nome, usuario, senha) 
                VALUES (@Nome, @Usuario, @Senha);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            int id = await connection.ExecuteScalarAsync<int>(sql, aluno);
            return id;
        }

        public async Task UpdateAlunoAsync(Aluno aluno)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE aluno SET Nome = @Nome, Usuario = @Usuario, Senha = @Senha WHERE Id = @Id";
            await connection.ExecuteAsync(sql, aluno);
        }

        public async Task DeleteAlunoAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE aluno SET IsActive = 0 WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Aluno com ID {id} foi desativado com sucesso.");
            }
            else
            {
                Console.WriteLine($"Nenhum aluno com ID {id} foi encontrado para desativar.");
            }
        }

        public async Task<AlunoTurma> GetRelacionamentoAsync(int alunoId, int turmaId)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM AlunoTurma WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";
            return await connection.QueryFirstOrDefaultAsync<AlunoTurma>(sql, new { AlunoId = alunoId, TurmaId = turmaId });
        }       

        public async Task<Aluno> GetByUsuarioAsync(string usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Aluno>("SELECT * FROM aluno WHERE Usuario = @Usuario", new { Usuario = usuario });
        }
    }
}
