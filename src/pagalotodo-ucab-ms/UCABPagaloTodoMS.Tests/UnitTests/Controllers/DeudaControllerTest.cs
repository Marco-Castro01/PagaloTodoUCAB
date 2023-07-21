using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using UCABPagaloTodoMS.Core.Entities;
using Xunit;

namespace UCABPagaloTodoMS.Tests.Controllers
{
    public class DeudaControllerTests
    {
        private readonly ILogger<DeudaController> _logger;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly DeudaController _controller;

        public DeudaControllerTests()
        {
            _logger = Mock.Of<ILogger<DeudaController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new DeudaController(_logger, _mediatorMock.Object);
        }

        [Fact]
        public async Task ListarDeudas_ReturnsOkResult_WhenQueryIsSuccessful()
        {
            // Arrange
            var request = new GetDeudaRequest();
            var idServicio = Guid.NewGuid();
            var response = new List<DeudaResponse>() { new DeudaResponse() };
            _mediatorMock.Setup(x => x.Send(It.IsAny<ConsultarDeudaQuery>(), default)).ReturnsAsync(response);

            // Act
            var result = await _controller.ListarDeudas(request, idServicio);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<List<DeudaResponse>>(okResult.Value);
            Assert.Equal(response, model);
        }

        [Fact]
        public async Task ListarDeudas_ReturnsBadRequest_WhenCustomExceptionIsThrown()
        {
            // Arrange
            var request = new GetDeudaRequest();
            var idServicio = Guid.NewGuid();
            var ex = new CustomException(StatusCodes.Status404NotFound, "Error personalizado");
            _mediatorMock.Setup(x => x.Send(It.IsAny<ConsultarDeudaQuery>(), default)).ThrowsAsync(ex);

            // Act
            var result = await _controller.ListarDeudas(request, idServicio);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(ex.Codigo, objectResult.StatusCode);
            Assert.Equal(ex.Message, objectResult.Value);
        }

        [Fact]
        public async Task ListarDeudas_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new GetDeudaRequest();
            var idServicio = Guid.NewGuid();
            var ex = new Exception("Error inesperado");
            _mediatorMock.Setup(x => x.Send(It.IsAny<ConsultarDeudaQuery>(), default)).ThrowsAsync(ex);

            // Act
            var result = await _controller.ListarDeudas(request, idServicio);

            // Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Error en la consulta", statusCodeResult.Value);
        }
    }
}