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
    public class ServicioControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ILogger<ServicioController> _logger;
        private readonly ServicioController _controller;

        public ServicioControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _logger = Mock.Of<ILogger<ServicioController>>();
            _controller = new ServicioController(_logger, _mediatorMock.Object);
        }

        [Fact]
        public async Task ConsultaServicio_ReturnsOkResult_WhenQueryIsSuccessful()
        {
            // Arrange
            var expectedResponse = new List<ServicioResponse> { new ServicioResponse { Id = Guid.NewGuid(), name = "Servicio 1" } };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ConsultarServicioPruebaQuery>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.ConsultaServicio();

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
            var actualResponse = Xunit.Assert.IsType<List<ServicioResponse>>(okResult.Value);
            Xunit.Assert.Equal(expectedResponse, actualResponse);
        }

        [Fact]
        public async Task ConsultaServicio_ReturnsBadRequestResult_WhenQueryThrowsException()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ConsultarServicioPruebaQuery>(), default)).ThrowsAsync(new Exception("Error de prueba"));

            // Act
            var result = await _controller.ConsultaServicio();

            // Assert
            var badResult = Xunit.Assert.IsType<BadRequestObjectResult>(result.Result);
            var expectedErrorMessage = "Error de prueba";
            Xunit.Assert.Equal(expectedErrorMessage, badResult.Value);
        }
    }
}
