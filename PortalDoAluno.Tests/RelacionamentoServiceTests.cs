using Moq;
using PortalDoAluno.Application.Services;
using PortalDoAluno.Domain.Interfaces;

namespace PortalDoAluno.Tests
{
    /// <summary>
    ///  Testes de unidade para RelacionamentoService - Implementado por Cristiane Fröhlich
    /// </summary>
    public class RelacionamentoServiceTests
    {
        private readonly RelacionamentoService _relacionamentoService;
        private readonly Mock<IAlunoRepository> _alunoRepositoryMock;
        private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
        private readonly Mock<IAlunoTurmaRepository> _alunoTurmaRepositoryMock;

        public RelacionamentoServiceTests()
        {
            // Inicializa os mocks
            _alunoRepositoryMock = new Mock<IAlunoRepository>();
            _turmaRepositoryMock = new Mock<ITurmaRepository>();
            _alunoTurmaRepositoryMock = new Mock<IAlunoTurmaRepository>();

            // Cria a instância do serviço com os mocks
            _relacionamentoService = new RelacionamentoService(
                _alunoRepositoryMock.Object,
                _turmaRepositoryMock.Object,
                _alunoTurmaRepositoryMock.Object);
        }

        [Fact]
        public async Task AssociarAlunoTurmaAsync_ShouldThrowException_WhenAssociacaoAtivaExists()
        {
            int alunoId = 1;
            int turmaId = 1;

            // Configura o mock para retornar uma associação ativa
            _alunoTurmaRepositoryMock
                .Setup(repo => repo.VerificarAssociacaoExistenteAsync(alunoId, turmaId))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _relacionamentoService.AssociarAlunoTurmaAsync(alunoId, turmaId));
        }

        [Fact]
        public async Task AssociarAlunoTurmaAsync_ShouldReactivate_WhenAssociacaoInativaExists()
        {
            int alunoId = 1;
            int turmaId = 1;

            // Configura o mock para não retornar uma associação ativa, mas uma inativa
            _alunoTurmaRepositoryMock
                .Setup(repo => repo.VerificarAssociacaoExistenteAsync(alunoId, turmaId))
                .ReturnsAsync(false);

            _alunoTurmaRepositoryMock
                .Setup(repo => repo.VerificarAssociacaoInativaAsync(alunoId, turmaId))
                .ReturnsAsync(true);

            await _relacionamentoService.AssociarAlunoTurmaAsync(alunoId, turmaId);

            _alunoTurmaRepositoryMock.Verify(repo => repo.ReativarAlunoNaTurmaAsync(alunoId, turmaId), Times.Once);
        }

        [Fact]
        public async Task AssociarAlunoTurmaAsync_ShouldCreateAssociation_WhenNoExistingAssociations()
        {
            int alunoId = 1;
            int turmaId = 1;

            // Configura o mock para não retornar nenhuma associação ativa ou inativa
            _alunoTurmaRepositoryMock
                .Setup(repo => repo.VerificarAssociacaoExistenteAsync(alunoId, turmaId))
                .ReturnsAsync(false);

            _alunoTurmaRepositoryMock
                .Setup(repo => repo.VerificarAssociacaoInativaAsync(alunoId, turmaId))
                .ReturnsAsync(false);

            await _relacionamentoService.AssociarAlunoTurmaAsync(alunoId, turmaId);

            _alunoTurmaRepositoryMock.Verify(repo => repo.RelacionarAlunoNaTurmaAsync(alunoId, turmaId), Times.Once);
        }

        [Fact]
        public async Task DesativarAlunoTurmaAsync_ShouldCallRepositoryMethod()
        {
            int alunoId = 1;
            int turmaId = 1;

            await _relacionamentoService.DesativarAlunoTurmaAsync(alunoId, turmaId);

            _alunoTurmaRepositoryMock.Verify(repo => repo.DesativarAlunoTurmaAsync(alunoId, turmaId), Times.Once);
        }

        [Fact]
        public async Task UpdateRelacionamentoAsync_ShouldReactivate_WhenAssociacaoInativaExists()
        {
            int alunoId = 1;
            int novaTurmaId = 2;

            // Configura o mock para não retornar uma associação ativa, mas uma inativa
            _alunoTurmaRepositoryMock
                .Setup(repo => repo.VerificarAssociacaoExistenteAsync(alunoId, novaTurmaId))
                .ReturnsAsync(false);

            _alunoTurmaRepositoryMock
                .Setup(repo => repo.VerificarAssociacaoInativaAsync(alunoId, novaTurmaId))
                .ReturnsAsync(true);

            await _relacionamentoService.UpdateRelacionamentoAsync(alunoId, novaTurmaId);

            _alunoTurmaRepositoryMock.Verify(repo => repo.ReativarAlunoNaTurmaAsync(alunoId, novaTurmaId), Times.Once);
        }
    }
}