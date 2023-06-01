using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Tests.DataSeed;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Handlers.Commands;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection.Metadata;
using Xunit;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Commands.AdminPrueba
{
    public class AgregarAdminPruebaCommandHandlerTests
    {

        private readonly AgregarAdminPruebaCommandHandler _handler;
        private readonly Mock<IUCABPagaloTodoDbContext> _contextMock;
        private readonly Mock<ILogger<ConsultarAdminQueryHandler>> _mockLogger;
        private readonly Mock<IDbContextTransactionProxy> _mockProxy;

        public AgregarAdminPruebaCommandHandlerTests()
        {
            _mockLogger = new Mock<ILogger<ConsultarAdminQueryHandler>>();
            _contextMock = new Mock<IUCABPagaloTodoDbContext>();
            _mockProxy = new Mock<IDbContextTransactionProxy>();
            _handler = new AgregarAdminPruebaCommandHandler(_contextMock.Object, _mockLogger.Object);
            _contextMock.SetupDbContextData();
        }

        [Fact]
        public async Task Handle_ReturnsExpectedGuid_WhenRequestIsValid()
        {
            // Arrange
            var request = new AgregarAdminPruebaCommand(new AdminRequest());
            var expectedGuid = Guid.NewGuid();
        }

        public class AgregarAdminPruebaCommandHandlerTests
        {
            private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
            private readonly Mock<ILogger<ConsultarAdminQueryHandler>> _loggerMock;
            private readonly AgregarAdminPruebaCommandHandler _agregarAdminPruebaCommandHandler;

            public AgregarAdminPruebaCommandHandlerTests()
            {
                _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
                _loggerMock = new Mock<ILogger<ConsultarAdminQueryHandler>>();
                _agregarAdminPruebaCommandHandler = new AgregarAdminPruebaCommandHandler(_dbContextMock.Object, _loggerMock.Object);
            }

            [Fact]
            public async Task HandleAsync_RequestIsNull_ThrowsArgumentNullException()
            {
                // Arrange
                var handler = new AgregarAdminPruebaCommandHandler();
                var request = new AgregarAdminPruebaCommand(null);

                // Act & Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(request, CancellationToken.None));
            }

            [Fact]
            public async Task HandleAsync_RequestIsNotNull_ReturnsGuid()
            {
                // Arrange
                var handlerMock = new Mock<AgregarAdminPruebaCommandHandler>() { CallBase = true };
                var request = new AgregarAdminPruebaCommand(new AgregarAdminPruebaRequest());

                // Mock the HandleAsync method to return a Guid
                var guid = Guid.NewGuid();
                handlerMock.Setup(x => x.HandleAsync(request)).ReturnsAsync(guid);

                // Act
                var result = await handlerMock.Object.Handle(request, CancellationToken.None);

                // Assert
                Assert.Equal(guid, result);
            }
        }
    }
}