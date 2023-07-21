/*using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Handlers.Commands;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using Xunit;

public class DeleteServicioPruebaCommandHandlerTests
{
    private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
    private readonly Mock<ILogger<DeleteServicioPruebaCommandHandler>> _loggerMock;
    private readonly DeleteServicioPruebaCommandHandler _handler;

    public int Id { get; private set; }

    public DeleteServicioPruebaCommandHandlerTests()
    {
        _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
        _loggerMock = new Mock<ILogger<DeleteServicioPruebaCommandHandler>>();
        _handler = new DeleteServicioPruebaCommandHandler(_dbContextMock.Object, _loggerMock.Object);
    }

    /*[Fact(DisplayName = "Handle_WithValidRequest_ShouldReturnGuid")]
    public async Task Handle_WithValidRequest_ShouldReturnGuid()
    {
        // Arrange
        var command = new DeleteCampoCommand(Guid.NewGuid());
        {
            Id = 1;
        };
        _dbContextMock.Setup(x => x.BeginTransaction()).Returns(new Mock<IDbContextTransaction>().Object);
        _dbContextMock.Setup(x => x.Servicio.Update(It.IsAny<ServicioEntity>()));
        _dbContextMock.Setup(x => x.SaveEfContextChanges("APP")).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(default(Guid), result);
        Assert.IsType<Guid>(result);
        // add more assertions as needed
    }

    [Fact(DisplayName = "Handle_WithNullRequest_ShouldThrowArgumentNullException")]
    public async Task Handle_WithNullRequest_ShouldThrowArgumentNullException()
    {
        // Arrange
        DeleteServicioPruebaCommand command = null;

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, CancellationToken.None));
    }

    // You could add more test cases to cover other scenarios if needed
}*/