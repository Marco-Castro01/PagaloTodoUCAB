using FluentValidation.Internal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using NetTopologySuite.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Mailing;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers { 
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
    [Fact(DisplayName = "Consultar")]
    public async Task ConsultaUsuarios_ReturnsOkResult()
    {
        // Arrange
        var query = new ConsultarUsuariosQuery();
        var response = new List<UsuariosAllResponse>();
        _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(response);

        // Act
        var result = await _controller.ConsultaUsuarios();

        // Assert
        var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
        var resultValue = Xunit.Assert.IsType<List<UsuariosAllResponse>>(okResult.Value);
        Xunit.Assert.Equal(response, resultValue);
    }
    [Fact(DisplayName = "Editar")]
    public async Task EditarUsuario_ReturnsOkResult()
    {
        // Arrange
        var request = new EditarUsuarioRequest();
        var command = new EditarUsuarioCommand(request);
        var response = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(response);

        // Act
        var result = await _controller.EditarUsuario(request);

        // Assert
        var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
        var resultValue = Xunit.Assert.IsType<Guid>(okResult.Value);
        Xunit.Assert.Equal(response, resultValue);
    }

    [Fact(DisplayName = "Agregar Admin")]
    public async Task Agregaradmin_ReturnsOkResult()
    {
        // Arrange
        var request = new AdminRequest();
        var command = new AgregarAdminCommand(request);
        var response = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(response);

        // Act
        var result = await _controller.Agregaradmin(request);

        // Assert
        var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
        var resultValue = Xunit.Assert.IsType<Guid>(okResult.Value);
        Xunit.Assert.Equal(response, resultValue);
    }

    [Fact(DisplayName = "Agregar Consumidor")]
    public async Task Agregarconsumidor_ReturnsOkResult()
    {
        // Arrange
        var request = new ConsumidorRequest();
        var command = new AgregarConsumidorCommand(request);
        var response = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(response);

        // Act
        var result = await _controller.Agregarconsumidor(request);

        // Assert
        var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
        var resultValue = Xunit.Assert.IsType<Guid>(okResult.Value);
        Xunit.Assert.Equal(response, resultValue);
    }

    [Fact(DisplayName = "Agregar Prestador")]
    public async Task Agregarprestador_ReturnsOkResult()
    {
        // Arrange
        var request = new PrestadorRequest();
        var command = new AgregarPrestadorCommand(request);
        var response = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(response);

        // Act
        var result = await _controller.Agregarprestador(request);

        // Assert
        var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
        var resultValue = Xunit.Assert.IsType<Guid>(okResult.Value);
        Xunit.Assert.Equal(response, resultValue);
    }

    [Fact (DisplayName = "Login")]
    public async Task Login_ReturnsOkResult()
    {
        // Arrange
        var request = new LoginRequest();
        var query = new LoginQuery(request);
        var response = "token";
        _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(response);

        // Act
        var result = await _controller.login(request);

        // Assert
        var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
        var resultValue = Xunit.Assert.IsType<string>(okResult.Value);
        Xunit.Assert.Equal(response, resultValue);
    }

    [Fact(DisplayName = "Reset Password")]
    public async Task Reset_ReturnsOkResult()
    {
        // Arrange
        var request = new ResetPasswordRequest();
        var query = new PasswordResetQuery(request);
        var response = new PasswordResetResponse();
        _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(response);

        // Act
        var result = await _controller.reset(request);

        // Assert
        var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
        var resultValue = Xunit.Assert.IsType<PasswordResetResponse>(okResult.Value);
        Xunit.Assert.Equal(response, resultValue);


    }

}
}
//faltaron 2