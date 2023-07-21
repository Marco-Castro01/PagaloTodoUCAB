using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using MediatR;
using Xunit;

namespace UCABPagaloTodoMS.Tests.Controllers
{
    public class PrestadorServicioControllerTests
    {
        [Fact]
        public async Task ConsultaPrestadorServicio_Returns_OkObjectResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<PrestadorServicioController>>();
            var controller = new PrestadorServicioController(loggerMock.Object, mediatorMock.Object);
            var expectedResult = new List<PrestadorServicioResponse> { new PrestadorServicioResponse() };
            var query = new ConsultarPrestadorServicioPruebaQuery();
            mediatorMock.Setup(x => x.Send(query, CancellationToken.None))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.ConsultaPrestadorServicio();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualResult = Assert.IsType<List<PrestadorServicioResponse>>(okResult.Value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task ConsultaPrestadorServicio_Returns_BadRequestObjectResult_On_CustomException()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<PrestadorServicioController>>();
            var controller = new PrestadorServicioController(loggerMock.Object, mediatorMock.Object);
            var exceptionMessage = "Custom Exception Message";
            var query = new ConsultarPrestadorServicioPruebaQuery();
            mediatorMock.Setup(x => x.Send(query, CancellationToken.None))
                .ThrowsAsync(new CustomException(exceptionMessage));

            // Act
            var result = await controller.ConsultaPrestadorServicio();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(exceptionMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task ConsultaPrestadorServicio_Returns_BadRequestObjectResult_On_Exception()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<PrestadorServicioController>>();
            var controller = new PrestadorServicioController(loggerMock.Object, mediatorMock.Object);
            var exceptionMessage = "Exception Message";
            var query = new ConsultarPrestadorServicioPruebaQuery();
            mediatorMock.Setup(x => x.Send(query, CancellationToken.None))
                .ThrowsAsync(new System.Exception(exceptionMessage));

            // Act
            var result = await controller.ConsultaPrestadorServicio();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(exceptionMessage, badRequestResult.Value);
        }
    }
}