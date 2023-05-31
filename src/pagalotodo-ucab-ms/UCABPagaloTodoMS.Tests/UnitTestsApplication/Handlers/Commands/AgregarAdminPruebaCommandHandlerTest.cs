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
            var request = new AgregarAdminPruebaCommand { /* initialize properties */ };
            var expectedGuid = Guid.NewGuid();
            
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
            public async Task Handle_ReturnsExpectedGuid_WhenRequestIsValid()
            {
                // Arrange
                var request = new AgregarAdminPruebaCommand { /* initialize properties */ };
                var expectedGuid = Guid.NewGuid();
                
public class AgregarAdminPruebaCommandHandlerTests
            {
                private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
                private readonly Mock<ILogger<AgregarAdminPruebaCommandHandler>> _loggerMock;
                private readonly AgregarAdminPruebaCommandHandler _agregarAdminPruebaCommandHandler;

                public AgregarAdminPruebaCommandHandlerTests()
                {
                    _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
                    _loggerMock = new Mock<ILogger<AgregarAdminPruebaCommandHandler>>();
                    _agregarAdminPruebaCommandHandler = new AgregarAdminPruebaCommandHandler(_dbContextMock.Object, _loggerMock.Object);
                }

                [Fact]
                public async Task Handle_ReturnsExpectedGuid_WhenRequestIsValid()
                {
                    // Arrange
                    var request = new AgregarAdminPruebaCommand { /* initialize properties */ };
                    var expectedGuid = Guid.NewGuid();
                    _dbContextMock
                        .Setup(m => m.BeginTransaction())
                        .Returns(new Mock<IDbContextTransaction>().Object);
                    _dbContextMock
                        .Setup(m => m.Admin.Add(It.IsAny<Admin>()))
                        .Callback<Admin>(e =>
                        {
                            e.IdSigo 
            
                    _dbContextMock
                        .Setup(m => m.BeginTransaction())
                        .Returns(new Mock<IDbContextTransaction>().Object);
                            _dbContextMock
                    .Setup(m => m.Admin.Add(It.IsAny<Admin>()))
                    .Callback<Admin>(e =>
                        {
                            e.Id = expectedGuid;
                        });
                            _dbContextMock
                    .Setup(m => m.SaveEfContextChanges("APP"))
                    .Returns(Task.CompletedTask);

                            // Act
                            var actualGuid = await _agregarAdminPruebaCommandHandler.Handle(request, CancellationToken.None);

                            // Assert
                            Assert.Equal(expectedGuid, actualGuid);
                        }

                    [Fact]
                public async Task Handle_ThrowsException_WhenDbContextThrowsException()
                    {
                        // Arrange
                        var request = new AgregarAdminPruebaCommand { /* initialize properties */ };
                        _dbContextMock
                            .Setup(m => m.BeginTransaction())
                            .Returns(new Mock<IDbContextTransaction>().Object);
                        _dbContextMock
                            .Setup(m => m.Admin.Add(It.IsAny<Admin>()))
                            .ThrowsException();
                        _dbContextMock
                            .Setup(m => m.SaveEfContextChanges("APP"))
                            .Returns(Task.CompletedTask);

                        // Act and Assert
                        await Assert.ThrowsAsync<Exception>(() => _agregarAdminPruebaCommandHandler.Handle(request, CancellationToken.None));
                    }
                }


            }


}
    }