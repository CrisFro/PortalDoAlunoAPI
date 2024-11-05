using Dapper;
using Microsoft.Extensions.Configuration;
using PortalDoAluno.Domain.Entities;
using PortalDoAluno.Domain.Interfaces;
using System.Data.SqlClient;

namespace PortalDoAluno.Infrastructure.Repositories
{

    public class TurmaRepository : ITurmaRepository
    {
        private readonly string _connectionString;

        public TurmaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PortalDoAlunoDB");
        }

        private SqlConnection GetConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Turma>> GetAllTurmasAsync()
        {
            using var connection = GetConnection();
            return await connection.QueryAsync<Turma>("SELECT * FROM turma WHERE IsActive = 1");
        }

        public async Task<Turma> GetTurmaByIdAsync(int id)
        {
            using var connection = GetConnection();
            return await connection.QueryFirstOrDefaultAsync<Turma>("SELECT * FROM turma WHERE id = @Id", new { Id = id });
        }

        public async Task<int> CreateTurmaAsync(Turma turma)
        {
            using var connection = GetConnection();
            var sql = @"
                INSERT INTO turma (curso_id, turma, ano, IsActive) 
                VALUES (@CursoId, @Nome, @Ano, @IsActive);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.ExecuteScalarAsync<int>(sql, new
            {
                turma.CursoId,
                turma.Nome,
                turma.Ano,
                turma.IsActive
            });
        }

        public async Task UpdateTurmaAsync(Turma turma)
        {
            using var connection = GetConnection();
            var sql = @"
                UPDATE turma 
                SET curso_id = @CursoId, turma = @Nome, ano = @Ano, IsActive = @IsActive
                WHERE id = @Id";

            await connection.ExecuteAsync(sql, new
            {
                turma.CursoId,
                turma.Nome,
                turma.Ano,
                turma.IsActive,
                turma.Id
            });
        }

        public async Task DeleteTurmaAsync(int id)
        {
            using var connection = GetConnection();
            var sql = "UPDATE turma SET IsActive = 0 WHERE id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
