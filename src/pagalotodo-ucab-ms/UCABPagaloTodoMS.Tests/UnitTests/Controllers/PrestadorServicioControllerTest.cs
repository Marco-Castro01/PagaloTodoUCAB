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
    public class PrestadorServicioControllerTest
    {
        private readonly PrestadorServicioController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<PrestadorServicioController>> _loggerMock;


        public PrestadorServicioControllerTest()
        {
            _loggerMock = new Mock<ILogger<PrestadorServicioController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new PrestadorServicioController(_loggerMock.Object, _mediatorMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();

        }





        [Fact(DisplayName = "Prestador good")]
        public async Task AgregarPrestadorServicio_ReturnsOkResponse_WhenPagoIsValid()
        {
            // Arrange
            var prestadorServicio = new PrestadorServicioRequest { /* initialize properties */ };
            var expectedGuid = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AgregarPrestadorServicioPruebaCommand>(), default))
                .ReturnsAsync(expectedGuid);

            // Act
            var result = await _controller.AgregarPrestadorServicio(prestadorServicio);

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Xunit.Assert.Equal(expectedGuid, okResult.Value);
        }

        [Fact(DisplayName = "Prestador bad")]
        public async Task AgregarPrestadorServicio_ThrowsException_WhenMediatorThrowsException()
        {
            // Arrange
            var prestadorServicio = new PrestadorServicioRequest { /* initialize properties */ };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AgregarPrestadorServicioPruebaCommand>(), default))
                .ThrowsAsync(new Exception());

            // Act and Assert
            await Xunit.Assert.ThrowsAsync<Exception>(() => _controller.AgregarPrestadorServicio(prestadorServicio));
        }

    }
}

