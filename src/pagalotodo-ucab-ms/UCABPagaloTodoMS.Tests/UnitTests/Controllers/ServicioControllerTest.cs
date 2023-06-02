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
    public class ServicioControllerTest
    {
        private readonly ServicioController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<ServicioController>> _loggerMock;


        public ServicioControllerTest()
        {
            _loggerMock = new Mock<ILogger<ServicioController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new ServicioController(_loggerMock.Object, _mediatorMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();

        }





        [Fact(DisplayName = "Servicio good")]
        public async Task AgregarServicio_ReturnsOkResponse_WhenPagoIsValid()
        {
            // Arrange
            var servicio = new ServicioRequest { /* initialize properties */ };
            var expectedGuid = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AgregarServicioPruebaCommand>(), default))
                .ReturnsAsync(expectedGuid);

            // Act
            var result = await _controller.AgregarServicio(servicio);

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Xunit.Assert.Equal(expectedGuid, okResult.Value);
        }

        [Fact(DisplayName = "Servicio bad")]
        public async Task AgregarsServicio_ThrowsException_WhenMediatorThrowsException()
        {
            // Arrange
            var servicio = new ServicioRequest { /* initialize properties */ };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AgregarServicioPruebaCommand>(), default))
                .ThrowsAsync(new Exception());

            // Act and Assert
            await Xunit.Assert.ThrowsAsync<Exception>(() => _controller.AgregarServicio(servicio));
        }

    }
}

