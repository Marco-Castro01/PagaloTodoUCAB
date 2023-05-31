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
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    public class AdminControllerTest
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


        /* [Fact(DisplayName = " Agregar Admin Prueba 1")]
         public async Task AgregarAdminPrueba()
         {
             var usuario = new AdminRequest
             {
                 cedula = "123456"
             };

             _mediatorMock.Setup(x => x.Send(It.IsAny<AgregarAdminPruebaCommand>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.FromResult<Guid>(new Guid()));

             var result = await _controller.AgregarAdmin(usuario);

             Xunit.Assert.IsType<OkObjectResult>(result);
             var okResult = result as OkObjectResult;
             object value = Xunit.Assert.NotNull(okResult);
             Xunit.Assert.Equals((int)HttpStatusCode.OK, okResult.StatusCode);
         }*/
        [Fact (DisplayName = "Agregar Admin good")]
        public async Task AgregarAdmin_ReturnsOkResponse_WhenAdminIsValid()
        {
            // Arrange
            var admin = new AdminRequest { /* initialize properties */ };
            var expectedGuid = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AgregarAdminPruebaCommand>(), default))
                .ReturnsAsync(expectedGuid);

            // Act
            var result = await _controller.AgregarAdmin(admin);

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Xunit.Assert.Equal(expectedGuid, okResult.Value);
        }

        [Fact(DisplayName = "Agregar Admin bad")]
        public async Task AgregarAdmin_ThrowsException_WhenMediatorThrowsException()
        {
            // Arrange
            var admin = new AdminRequest { /* initialize properties */ };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AgregarAdminPruebaCommand>(), default))
                .ThrowsAsync(new Exception());

            // Act and Assert
            await Xunit.Assert.ThrowsAsync<Exception>(() => _controller.AgregarAdmin(admin));
        }
    }
}



