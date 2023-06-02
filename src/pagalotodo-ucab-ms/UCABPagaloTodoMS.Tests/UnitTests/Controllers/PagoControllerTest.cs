/*using FluentValidation.Internal;
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
    public class PagoControllerTest
    {
        private readonly PagoController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<PagoController>> _loggerMock;


        public PagoControllerTest()
        {
            _loggerMock = new Mock<ILogger<PagoController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new PagoController(_loggerMock.Object, _mediatorMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();

        }





        [Fact(DisplayName = "pagoDirecto good")]
        public async Task AgregarPago_ReturnsOkResponse_WhenPagoIsValid()
        {
            // Arrange
            var pago = new PagoDirectoRequest {  };
            var expectedGuid = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AgregarPagoDirectoCommand>(), default))
                .ReturnsAsync(expectedGuid);

            // Act
            var result = await _controller.AgregarPago(pago);

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Xunit.Assert.Equal(expectedGuid, okResult.Value);
        }

        [Fact(DisplayName = "pagoDirecto Bad")]
        public async Task AgregarPago_ThrowsException_WhenMediatorThrowsException()
        {
            // Arrange
            var pago = new PagoDirectoRequest { };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AgregarPagoDirectoCommand>(), default))
                .ThrowsAsync(new Exception());

            // Act and Assert
            await Xunit.Assert.ThrowsAsync<Exception>(() => _controller.AgregarPago(pago));
        }

    }
}

*/