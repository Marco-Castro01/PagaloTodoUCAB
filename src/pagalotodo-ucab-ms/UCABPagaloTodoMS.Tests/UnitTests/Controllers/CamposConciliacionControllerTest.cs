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
    public class CamposConciliacionControllerTest
    {
        private readonly CamposConciliacionController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<CamposConciliacionController>> _loggerMock;


        public CamposConciliacionControllerTest()
        {
            _loggerMock = new Mock<ILogger<CamposConciliacionController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new CamposConciliacionController(_loggerMock.Object, _mediatorMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();

        }


 
     

            [Fact(DisplayName = "Campo Good")]
            public async Task AgregarCamposConciliacion_ReturnsOkResponse_WhenCamposConciliacionIsValid()
            {
                // Arrange
                var camposConciliacion = new CamposConciliacionRequest { };
                var expectedGuid = Guid.NewGuid();
                _mediatorMock
                    .Setup(m => m.Send(It.IsAny<AgregarCamposConciliacionPruebaCommand>(), default))
                    .ReturnsAsync(expectedGuid);

                // Act
                var result = await _controller.AgregarCamposConciliacion(camposConciliacion);

                // Assert
                Xunit.Assert.IsType<OkObjectResult>(result.Result);
                var okResult = result.Result as OkObjectResult;
                Xunit.Assert.Equal(expectedGuid, okResult.Value);
            }

            [Fact(DisplayName = "Campo Bad")]
            public async Task AgregarCamposConciliacion_ThrowsException_WhenMediatorThrowsException()
            {
                // Arrange
                var camposConciliacion = new CamposConciliacionRequest { };
                _mediatorMock
                    .Setup(m => m.Send(It.IsAny<AgregarCamposConciliacionPruebaCommand>(), default))
                    .ThrowsAsync(new Exception());

            // Act and Assert
            await Xunit.Assert.ThrowsAsync<Exception>(() => _controller.AgregarCamposConciliacion(camposConciliacion));
            }
        }
    }


*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    public class CamposConciliacionControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<CamposConciliacionController>> _loggerMock;
        private readonly CamposConciliacionController _controller;

        public CamposConciliacionControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<CamposConciliacionController>>();
            _controller = new CamposConciliacionController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task ConsultaCamposConciliacion_Returns_Ok_With_CamposConciliacionResponseList()
        {
            // Arrange
            var expectedResponse = new List<CamposConciliacionResponse>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<ConsultarCamposConciliacionPruebaQuery>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.ConsultaCamposConciliacion();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ConsultarCamposConciliacionPruebaQuery>(), default), Times.Once);
        }

        [Fact]
        public async Task AgregarCamposConciliacion_Returns_Ok_With_Guid()
        {
            // Arrange
            var campo = new CamposConciliacionRequest();
            var expectedId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<AgregarCamposConciliacionCommand>(), default)).ReturnsAsync(expectedId);

            // Act
            var result = await _controller.AgregarCamposConciliacion(campo);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(expectedId, okResult.Value);
            _mediatorMock.Verify(m => m.Send(It.IsAny<AgregarCamposConciliacionCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task ConsultaCamposConciliacion_Returns_BadRequest_When_Exception_Is_Thrown()
        {
            // Arrange
            var exception = new Exception("Test exception");
            _mediatorMock.Setup(m => m.Send(It.IsAny<ConsultarCamposConciliacionPruebaQuery>(), default)).ThrowsAsync(exception);

            // Act
            var result = await _controller.ConsultaCamposConciliacion();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal(exception.Message, badRequestResult.Value);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ConsultarCamposConciliacionPruebaQuery>(), default), Times.Once);
        }

        [Fact]
        public async Task AgregarCamposConciliacion_Returns_BadRequest_When_Exception_Is_Thrown()
        {
            // Arrange
            var exception = new Exception("Test exception");
            var campo = new CamposConciliacionRequest();
            _mediatorMock.Setup(m => m.Send(It.IsAny<AgregarCamposConciliacionCommand>(), default)).ThrowsAsync(exception);

            // Act
            var result = await _controller.AgregarCamposConciliacion(campo);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal(exception.Message, badRequestResult.Value);
            _mediatorMock.Verify(m => m.Send(It.IsAny<AgregarCamposConciliacionCommand>(), default), Times.Once);
        }
    }
}