using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Mailing;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using Xunit;

public class UsuarioControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IEmailSender> _emailSenderMock;
    private readonly usuarioController _controller;

    public UsuarioControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _emailSenderMock = new Mock<IEmailSender>();
        _controller = new usuarioController(
            Mock.Of<ILogger<usuarioController>>(),
            _mediatorMock.Object,
            _emailSenderMock.Object);
    }

    [Fact]
    public async Task ConsultaUsuarios_ReturnsOkResponse()
    {
        // Arrange
        var expectedResponse = new List<UsuariosAllResponse>()
        {
            // Lista de usuarios esperada
        };
        _mediatorMock
            .Setup(x => x.Send(It.IsAny<ConsultarUsuariosQuery>(), default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.ConsultaUsuarios();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualResponse = Assert.IsAssignableFrom<List<UsuariosAllResponse>>(okResult.Value);
        Assert.Equal(expectedResponse.Count, actualResponse.Count);
    }
}