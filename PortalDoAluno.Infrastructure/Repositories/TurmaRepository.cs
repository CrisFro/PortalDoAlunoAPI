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
            var sql = "SELECT id, curso_id AS CursoId, turma AS Nome, ano, isActive FROM turma WHERE isActive = 1";
            return await connection.QueryAsync<Turma>(sql);
        }


        public async Task<Turma> GetTurmaByIdAsync(int id)
        {
            using var connection = GetConnection();
            var sql = "SELECT id, curso_id AS CursoId, turma AS Nome, ano, isActive FROM turma WHERE id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Turma>(sql, new { Id = id });
        }

        public async Task<int> CreateTurmaAsync(Turma turma)
        {
            using var connection = GetConnection();
            var sql = @"
                INSERT INTO turma (curso_id, turma, ano, isActive) 
                VALUES (@CursoId, @Nome, @Ano, @IsActive);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.ExecuteScalarAsync<int>(sql, turma);
        }

        public async Task UpdateTurmaAsync(Turma turma)
        {
            using var connection = GetConnection();
            var sql = @"
                UPDATE turma 
                SET curso_id = @CursoId, turma = @Nome, ano = @Ano, isActive = @IsActive
                WHERE id = @Id";
            await connection.ExecuteAsync(sql, turma);
        }

        public async Task DeleteTurmaAsync(int id)
        {
            using var connection = GetConnection();
            var sql = "UPDATE turma SET isActive = 0 WHERE id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<Turma> GetTurmaByNomeAsync(string nome)
        {
            using var connection = GetConnection();
            var sql = "SELECT id, curso_id AS CursoId, turma AS Nome, ano, isActive FROM turma WHERE turma = @Nome AND isActive = 1";
            return await connection.QueryFirstOrDefaultAsync<Turma>(sql, new { Nome = nome });
        }
    }
}
