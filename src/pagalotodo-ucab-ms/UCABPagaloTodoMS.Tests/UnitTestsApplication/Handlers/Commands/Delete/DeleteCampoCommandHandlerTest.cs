/*using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Commands;
using UCABPagaloTodoMS.Core.Database;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Commands.Delete
{
    public class DeleteCampoCommandHandlerTests
    {
        private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
        private readonly Mock<ILogger<DeleteCampoCommandHandler>> _loggerMock;
        private readonly DeleteCampoCommandHandler _handler;

        public int Id { get; private set; }

        public DeleteCampoCommandHandlerTests()
        {
            _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
            _loggerMock = new Mock<ILogger<DeleteCampoCommandHandler>>();
            _handler = new DeleteCampoCommandHandler(_dbContextMock.Object, _loggerMock.Object);
        }

        [Fact(DisplayName = "Handle_WithValidRequest_ShouldReturnString")]
        public async Task Handle_WithValidRequest_ShouldReturnString()
        {
            // Arrange
            var command = new DeleteCampoCommand(Guid.NewGuid());
            {
                Id = 1;
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
       
        }

        [Fact(DisplayName = "Handle_WithNullRequest_ShouldThrowArgumentNullException")]
        public async Task Handle_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            DeleteCampoCommand command = null;

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}*/