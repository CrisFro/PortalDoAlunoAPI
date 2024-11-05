using Moq;
using PortalDoAluno.Application.Services;
using PortalDoAluno.Domain.Entities;
using PortalDoAluno.Domain.Interfaces;

namespace PortalDoAluno.Tests
{
    /// Testes de unidade para AlunoService - Implementado por Cristiane Fröhlich

    public class AlunoServiceTests
    {
        private readonly AlunoService _alunoService;
        private readonly Mock<IAlunoRepository> _alunoRepositoryMock;

        public AlunoServiceTests()
        {
            _alunoRepositoryMock = new Mock<IAlunoRepository>();
            _alunoService = new AlunoService(_alunoRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAlunosAsync_ShouldReturnListOfAlunos()
        {
            // Teste para verificar se retorna uma lista de alunos
            var alunos = new List<Aluno>
            {
                new Aluno
                {
                    Id = 1,
                    Nome = "Cristiane",
                    Usuario = "cris123",
                    Senha = "senha123"
                }
            };
            _alunoRepositoryMock.Setup(repo => repo.GetAllAlunosAsync()).ReturnsAsync(alunos);

            var result = await _alunoService.GetAllAlunosAsync();
            var resultList = result.ToList();

            Assert.NotNull(resultList);
            Assert.Single(resultList);
            Assert.Equal("Cristiane", resultList[0].Nome);
        }

        [Fact]
        public async Task GetAlunoByIdAsync_ShouldReturnAluno_WhenAlunoExists()
        {
            // Teste para verificar se retorna o aluno correto
            var aluno = new Aluno { Nome = "Cristiane", Usuario = "cris123", Senha = "senha123" };
            _alunoRepositoryMock.Setup(repo => repo.GetAlunoByIdAsync(1)).ReturnsAsync(aluno);

            var result = await _alunoService.GetAlunoByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Cristiane", result.Nome);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnAlunoWithId()
        {
            // Teste para verificar se retorna o aluno com ID atribuído
            var aluno = new Aluno { Nome = "Cristiane", Usuario = "cris123", Senha = "senha123" };
            _alunoRepositoryMock.Setup(repo => repo.CreateAlunoAsync(aluno)).ReturnsAsync(1);

            var result = await _alunoService.CreateAsync(aluno);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Cristiane", result.Nome);
        }
    }
}
