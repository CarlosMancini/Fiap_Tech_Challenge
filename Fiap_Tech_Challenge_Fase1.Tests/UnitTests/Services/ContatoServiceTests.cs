using Core.Entities;
using Core.Gateways;
using Core.Interfaces.Repository;
using Fiap_Tech_Challenge_Fase1.Services;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Fiap_Tech_Challenge_Fase1.Tests.UnitTests.Services
{
    public class ContatoServiceTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ContatoService _contatoService;
        private readonly Mock<IRegiaoRepository> _regiaoRepositoryMock;
        private readonly Mock<IContatoRepository> _contatoRepositoryMock;
        private readonly Mock<IBrasilGateway> _brasilGatewayMock;

        public ContatoServiceTests()
        {
            // Configurar o DbContext para usar o banco de dados em mem�ria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ApplicationDbContext(options);

            _regiaoRepositoryMock = new Mock<IRegiaoRepository>();
            _contatoRepositoryMock = new Mock<IContatoRepository>();
            _brasilGatewayMock = new Mock<IBrasilGateway>();

            // Inicializar o servi�o de Contato com reposit�rios mockados
            _contatoService = new ContatoService(_contatoRepositoryMock.Object, _regiaoRepositoryMock.Object, _brasilGatewayMock.Object);
        }

        [Fact]
        public async Task Cadastrar_DeveLancarException_QuandoContatoJaExiste()
        {
            // Arrange
            var contatoExistente = new Contato
            {
                ContatoNome = "Teste Contato",
                ContatoEmail = "teste@contato.com",
                ContatoTelefone = "61956325874",
                RegiaoId = 1
            };

            // Configurar o reposit�rio para retornar o contato existente
            _contatoRepositoryMock.Setup(repo => repo.ObterPorNomeETelefone(It.IsAny<string>(), It.IsAny<string>()))
                                  .ReturnsAsync(contatoExistente);

            var novoContato = new Contato
            {
                ContatoNome = "Teste Contato",
                ContatoEmail = "outro@contato.com", // Mesmo nome e telefone
                ContatoTelefone = "61956325874",
                RegiaoId = 1
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _contatoService.Cadastrar(novoContato));
            Assert.Equal("Ja existe um contato com o mesmo nome e telefone.", exception.Message);
        }

        [Fact]
        public async Task Cadastrar_DeveCadastrarNovoContato_QuandoNaoExisteDuplicata()
        {
            // Arrange
            var novoContato = new Contato
            {
                ContatoNome = "Teste Novo Contato",
                ContatoEmail = "novo@contato.com",
                ContatoTelefone = "117654321",
                RegiaoId = 1
            };

            // Configurar o reposit�rio para n�o encontrar nenhum contato duplicado
            _contatoRepositoryMock.Setup(repo => repo.ObterPorNomeETelefone(It.IsAny<string>(), It.IsAny<string>()))
                                  .ReturnsAsync((Contato)null);

            // Configurar o reposit�rio para encontrar a regi�o existente
            var regiaoExistente = new Regiao
            {
                Id = 1,
                RegiaoNome = "S�o Paulo",
                RegiaoDdd = 11
            };
            _regiaoRepositoryMock.Setup(repo => repo.ObterTodos())
                                 .ReturnsAsync(new List<Regiao> { regiaoExistente });

            // Act
            await _contatoService.Cadastrar(novoContato);

            // Assert
            _contatoRepositoryMock.Verify(repo => repo.Cadastrar(It.IsAny<Contato>()), Times.Once);
        }
    }
}

