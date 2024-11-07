using Moq;
using PortalDoAluno.Application.DTOs;
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
                    CursoId = 1,
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
                CursoId = 1,
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
            var turmaDto = new TurmaDto
            {
                CursoId = 1,
                Nome = "Turma de Ciência da Computação",
                Ano = 2023,
                IsActive = true
            };

            var turma = new Turma
            {
                Id = 1,
                CursoId = 1,
                Nome = "Turma de Ciência da Computação",
                Ano = 2023,
                IsActive = true
            };

            _turmaRepositoryMock.Setup(repo => repo.CreateTurmaAsync(It.IsAny<Turma>())).ReturnsAsync(1);

            var result = await _turmaService.CreateTurmaAsync(turmaDto);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Turma de Ciência da Computação", result.Nome);
        }

        [Fact]
        public async Task UpdateTurmaAsync_ShouldInvokeRepositoryMethod()
        {
            // Teste para verificar se o método de atualização é chamado
            var turmaDto = new TurmaDto
            {
                Id = 1,
                CursoId = 1,
                Nome = "Turma Atualizada",
                Ano = 2024,
                IsActive = true
            };

            await _turmaService.UpdateTurmaAsync(turmaDto);

            _turmaRepositoryMock.Verify(repo => repo.UpdateTurmaAsync(It.IsAny<Turma>()), Times.Once);
        }

        [Fact]
        public async Task DeleteTurmaAsync_ShouldInvokeRepositoryMethod()
        {
            int turmaId = 2; 
            var existingTurma = new Turma
            {
                Id = turmaId,
                CursoId = 2,
                Nome = "Engenharia de Software",
                Ano = 2023,
                IsActive = true
            };

            _turmaRepositoryMock.Setup(repo => repo.GetTurmaByIdAsync(turmaId))
                .ReturnsAsync(existingTurma);

            _turmaRepositoryMock.Setup(repo => repo.UpdateTurmaAsync(It.IsAny<Turma>()))
                .Returns(Task.CompletedTask);

            await _turmaService.DeleteTurmaAsync(turmaId);

            _turmaRepositoryMock.Verify(repo => repo.GetTurmaByIdAsync(turmaId), Times.Once);
            _turmaRepositoryMock.Verify(repo => repo.UpdateTurmaAsync(It.Is<Turma>(t => t.IsActive == false)), Times.Once);
        }
    }
}
