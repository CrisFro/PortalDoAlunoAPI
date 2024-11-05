using Moq;
using PortalDoAluno.Application.Services;
using PortalDoAluno.Domain.Entities;
using PortalDoAluno.Domain.Interfaces;

namespace PortalDoAluno.Tests
{
    /// Testes de unidade para TurmaService - Implementado por Cristiane Fröhlich
    public class TurmaServiceTests
    {
        private readonly TurmaService _turmaService;
        private readonly Mock<ITurmaRepository> _turmaRepositoryMock;

        public TurmaServiceTests()
        {
            _turmaRepositoryMock = new Mock<ITurmaRepository>();
            _turmaService = new TurmaService(_turmaRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllTurmasAsync_ShouldReturnListOfTurmas()
        {
            // Teste para verificar se retorna uma lista de turmas
            var turmas = new List<Turma>
            {
                new Turma
                {
                    Id = 1,
                    CursoId = (Curso)1,                    
                    Nome = "Turma de Ciência da Computação",
                    Ano = 2023,
                    IsActive = true
                }
            };
            _turmaRepositoryMock.Setup(repo => repo.GetAllTurmasAsync()).ReturnsAsync(turmas);

            var result = await _turmaService.GetAllTurmasAsync();
            var resultList = result.ToList();

            Assert.NotNull(resultList);
            Assert.Single(resultList);
            Assert.Equal("Turma de Ciência da Computação", resultList[0].Nome);
        }

        [Fact]
        public async Task GetTurmaByIdAsync_ShouldReturnTurma_WhenTurmaExists()
        {
            // Teste para verificar se retorna a turma correta
            var turma = new Turma
            {
                Id = 1,
                CursoId = (Curso)1,
                Nome = "Turma de Ciência da Computação",
                Ano = 2023,
                IsActive = true
            };
            _turmaRepositoryMock.Setup(repo => repo.GetTurmaByIdAsync(1)).ReturnsAsync(turma);

            var result = await _turmaService.GetTurmaByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Turma de Ciência da Computação", result.Nome);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task CreateTurmaAsync_ShouldReturnTurmaWithId()
        {
            // Teste para verificar se cria a turma com ID atribuído
            var turma = new Turma
            {
                CursoId = (Curso)1,
                Nome = "Turma de Ciência da Computação",
                Ano = 2023,
                IsActive = true
            };
            _turmaRepositoryMock.Setup(repo => repo.CreateTurmaAsync(turma)).ReturnsAsync(1);

            var result = await _turmaService.CreateTurmaAsync(turma);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Turma de Ciência da Computação", result.Nome);
        }

        [Fact]
        public async Task UpdateTurmaAsync_ShouldInvokeRepositoryMethod()
        {
            // Teste para verificar se o método de atualização é invocado
            var turma = new Turma
            {
                Id = 1,
                CursoId = (Curso)1,
                Nome = "Turma Atualizada",
                Ano = 2024,
                IsActive = true
            };

            await _turmaService.UpdateTurmaAsync(turma);

            _turmaRepositoryMock.Verify(repo => repo.UpdateTurmaAsync(turma), Times.Once);
        }

        [Fact]
        public async Task DeleteTurmaAsync_ShouldInvokeRepositoryMethod()
        {
            // Teste para verificar se o método de exclusão é invocado
            var turmaId = 1;

            await _turmaService.DeleteTurmaAsync(turmaId);

            _turmaRepositoryMock.Verify(repo => repo.DeleteTurmaAsync(turmaId), Times.Once);
        }
    }
}
