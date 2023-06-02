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
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    /* public class AdminControllerTest
     {
         private readonly AdminController _controller;
         private readonly Mock<IMediator> _mediatorMock;
         private readonly Mock<ILogger<AdminController>> _loggerMock;


         public AdminControllerTest()
         {
             _loggerMock = new Mock<ILogger<AdminController>>();
             _mediatorMock = new Mock<IMediator>();
             _controller = new AdminController(_loggerMock.Object, _mediatorMock.Object);
             _controller.ControllerContext = new ControllerContext();
             _controller.ControllerContext.HttpContext = new DefaultHttpContext();
             _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();

         }





         [Fact (DisplayName = "Agregar Admin good")]
         public async Task AgregarAdmin_ReturnsOkResponse_WhenAdminIsValid()
         {
             // Arrange
             var admin = new AdminRequest { };
             var expectedGuid = Guid.NewGuid();
             _mediatorMock
                 .Setup(m => m.Send(It.IsAny<AgregarAdminCommand>(), default))
                 .ReturnsAsync(expectedGuid);

             // Act
             var result = await _controller.Admin(admin);

             // Assert
             Xunit.Assert.IsType<OkObjectResult>(result.Result);
             var okResult = result.Result as OkObjectResult;
             Xunit.Assert.Equal(expectedGuid, okResult.Value);
         }


         [Fact(DisplayName = "Agregar Admin bad")]
         public async Task AgregarAdmin_ThrowsException_WhenMediatorThrowsException()
         {
             // Arrange
             var admin = new AdminRequest { };
             _mediatorMock
                 .Setup(m => m.Send(It.IsAny<AgregarAdminCommand>(), default))
                 .ThrowsAsync(new Exception());

             // Act and Assert
             await Xunit.Assert.ThrowsAsync<Exception>(() => _controller.AgregarAdmin(admin));
         }*/
}

public class AdminsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<AdminController>> _loggerMock;
    private readonly AdminController _controller;

    public AdminsControllerTests()
    {
        _loggerMock = new Mock<ILogger<AdminController>>();
        _mediatorMock = new Mock<IMediator>();
        _controller = new AdminController(_loggerMock.Object, _mediatorMock.Object);
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
    }

    [Fact(DisplayName = "prueba unitaria 1 de admin good")]
    public async Task ConsultaAdmins_ReturnsOkResult()
    {
        // Arrange
        var response = new List<AdminResponse>()
        {
            new AdminResponse() { cedula = "123456789", nickName = "admin1",  email = "admin1@example.com", status = false, password = "123456789"  },
            new AdminResponse() {  cedula = "87654321", nickName = "admin2",  email = "admin2@example.com", status = true , password = "123456789"},
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<ConsultarAdminPruebaQuery>(), default)).ReturnsAsync(response);

        // Act
        var result = await _controller.ConsultaAdmins();

        // Assert
        var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
        var model = Xunit.Assert.IsAssignableFrom<List<AdminResponse>>(okResult.Value);
        Xunit.Assert.Equal(response.Count, model.Count);
    }

    [Fact(DisplayName = "prueba unitaria 1 de admin bad")]
    public async Task ConsultaAdmins_ReturnsBadRequestResult()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<ConsultarAdminPruebaQuery>(), default)).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.ConsultaAdmins();

        // Assert
        Xunit.Assert.IsType<BadRequestResult>(result.Result);
    }
}
