using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using Xunit;

namespace UCABPagaloTodoMS.Tests.Controllers
{
    public class ConsumidorControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly ConsumidorController _controller;

        public ConsumidorControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new ConsumidorController(Mock.Of<ILogger<ConsumidorController>>(), _mockMediator.Object);
        }

        [Fact]
        public async Task ConsultaConsumidores_DebeRetornarListaConsumidores()
        {
            // Arrange
            var expectedResponse = new List<ConsumidorResponse>
            {
                new ConsumidorResponse { Id = Guid.NewGuid(), name = "Consumidor 1" },
                new ConsumidorResponse { Id = Guid.NewGuid(), name = "Consumidor 2" }
            };
            _mockMediator.Setup(x => x.Send(It.IsAny<ConsultarConsumidorPruebaQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.ConsultaConsumidores();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualResponse = Assert.IsAssignableFrom<List<ConsumidorResponse>>(okResult.Value);
            Assert.Equal(expectedResponse.Count, actualResponse.Count);
            Assert.Equal(expectedResponse[0].Id, actualResponse[0].Id);
            Assert.Equal(expectedResponse[0].name, actualResponse[0].name);
            Assert.Equal(expectedResponse[1].Id, actualResponse[1].Id);
            Assert.Equal(expectedResponse[1].name, actualResponse[1].name);
        }

        [Fact]
        public async Task getInfoConsumidor_DebeRetornarInfoConsumidor()
        {
            // Arrange
            // Arrange
            var expectedResponse = new List<ConsumidorResponse>
{
    new ConsumidorResponse { Id = Guid.NewGuid(), name = "Consumidor 1" },
    new ConsumidorResponse { Id = Guid.NewGuid(), name = "Consumidor 2" }
};
            _mockMediator.Setup(x => x.Send(It.IsAny<ConsultarConsumidorPruebaQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResponse));

            // Act
            var result = await _controller.ConsultaConsumidores();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualResponse = Assert.IsAssignableFrom<List<ConsumidorResponse>>(okResult.Value);
            Assert.Equal(expectedResponse.Count, actualResponse.Count);
            Assert.Equal(expectedResponse[0].Id, actualResponse[0].Id);
            Assert.Equal(expectedResponse[0].name, actualResponse[0].name);
            Assert.Equal(expectedResponse[1].Id, actualResponse[1].Id);
            Assert.Equal(expectedResponse[1].name, actualResponse[1].name);
        }

        [Fact]
        public async Task getInfoConsumidor_DebeRetornarErrorSiUsuarioNoEstaLogueado()
        {
            // Arrange
            var identity = new ClaimsIdentity(new List<Claim>());
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = await _controller.getInfoConsumidor();
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Null(objectResult.Value);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(422, objectResult.StatusCode);
        }

        /*[Fact]
        public async Task getInfoConsumidor_DebeRetornarErrorSiOcurreExcepcion()
        {
            // Arrange
            Guid consumidorId = Guid.NewGuid();
            string userId = consumidorId.ToString();
            var identity = new ClaimsIdentity(new List<Claim> { new Claim("Id", userId) });
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
            _mockMediator.Setup(x => x.Send(It.IsAny<GetInfoConsumidorQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("No se encontró el consumidor solicitado"));

            // Act
            var result = await _controller.getInfoConsumidor();

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(404, statusCodeResult.StatusCode);
        }*/
    }
}