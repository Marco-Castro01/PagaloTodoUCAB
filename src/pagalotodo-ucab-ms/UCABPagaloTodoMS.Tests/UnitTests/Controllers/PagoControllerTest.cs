using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using Xunit;

public class PagoControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<PagoController>> _loggerMock;
    private readonly PagoController _controller;

    public PagoControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<PagoController>>();

        _controller = new PagoController(_loggerMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task ConsultaPago_ReturnsListOfPagoResponse_WhenSuccessful()
    {
        // Arrange
        var query = new ConsultarPagoPruebaQuery();
        var expectedResponse = new List<PagoResponse>
    {
        new PagoResponse { Id = Guid.NewGuid(), valor = 1500},
        new PagoResponse { Id = Guid.NewGuid(), valor = 2000 }
    };
        _mediatorMock.Setup(x => x.Send(query, default)).ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.ConsultaPago();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResponse = Assert.IsType<List<PagoResponse>>(okResult.Value);
        Assert.Equal(expectedResponse.Count, actualResponse.Count);
        Assert.Equal(expectedResponse[0].Id, actualResponse[0].Id);
        Assert.Equal(expectedResponse[0].valor, actualResponse[0].valor);
        Assert.Equal(expectedResponse[1].Id, actualResponse[1].Id);
        Assert.Equal(expectedResponse[1].valor, actualResponse[1].valor);
    }

    [Fact]
    public async Task ListarPagosPorIdConsumidor_ReturnsBadRequest_WhenUserIdIsNull()
    {
        // Arrange
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity())
            }
        };

        // Act
        var result = await _controller.ListarPagosPorIdConsumidor();

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
        Assert.Equal(StatusCodes.Status422UnprocessableEntity, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task AgregarPagoDirecto_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var request = new PagoDirectoRequest();
        var idServicio = Guid.NewGuid();
        var idConsumidor = Guid.NewGuid();
        var expectedResponse = Guid.NewGuid();
        var command = new AgregarPagoDirectoCommand(request, idServicio, idConsumidor);
        _mediatorMock.Setup(x => x.Send(command, default)).ReturnsAsync(expectedResponse);

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", idConsumidor.ToString()),
                    new Claim(ClaimTypes.Role, "ConsumidorEntity")
                }, "mock"))
            }
        };

        // Act
        var result = await _controller.AgregarPagoDirecto(request, idServicio);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal("Pago Exitoso", okResult.Value);
    }
}