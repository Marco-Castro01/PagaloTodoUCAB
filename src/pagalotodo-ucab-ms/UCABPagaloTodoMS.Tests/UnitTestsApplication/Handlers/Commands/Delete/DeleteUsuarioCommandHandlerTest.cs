/*using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Commands;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using Xunit;

namespace UCABPagaloTodoMS.Application.Tests.Handlers.Commands
{
    public class DeleteUsuarioCommandHandlerTests
    {
        private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
        private readonly Mock<ILogger<DeleteUsuarioCommandHandler>> _loggerMock;
        private readonly DeleteUsuarioCommandHandler _handler;

        public DeleteUsuarioCommandHandlerTests()
        {
            _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
            _loggerMock = new Mock<ILogger<DeleteUsuarioCommandHandler>>();
            _handler = new DeleteUsuarioCommandHandler(_dbContextMock.Object, _loggerMock.Object);
        }

        /*[Fact]
        public async Task Handle_WithValidRequest_ShouldReturnString()
        {
            // Arrange
            var command = new DeleteUsuarioCommand(Guid.NewGuid());
            _dbContextMock.Setup(x => x.BeginTransaction()).Returns(new Mock<IDbContextTransaction>().Object);
            _dbContextMock.Setup(x => x.Usuarios.Update(It.IsAny<UsuarioEntity>()));
            _dbContextMock.Setup(x => x.SaveChanges());
            _dbContextMock.Setup(x => x.SaveEfContextChanges("APP")).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("Eliminacion exitosa", result);
            // add more assertions as needed
        }

        [Fact]
        public async Task Handle_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            DeleteUsuarioCommand command = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithCustomException_ShouldThrowCustomException()
        {
            // Arrange
            var command = new DeleteUsuarioCommand(Guid.NewGuid());
            _dbContextMock.Setup(x => x.BeginTransaction()).Throws(new CustomException("Custom exception message"));

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithException_ShouldThrowException()
        {
            // Arrange
            var command = new DeleteUsuarioCommand(Guid.NewGuid());
            _dbContextMock.Setup(x => x.BeginTransaction()).Throws(new Exception("Exception message"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}*/