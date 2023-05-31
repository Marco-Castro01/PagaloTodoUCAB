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
                var camposConciliacion = new CamposConciliacionRequest { /* initialize properties */ };
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
                var camposConciliacion = new CamposConciliacionRequest { /* initialize properties */ };
                _mediatorMock
                    .Setup(m => m.Send(It.IsAny<AgregarCamposConciliacionPruebaCommand>(), default))
                    .ThrowsAsync(new Exception());

            // Act and Assert
            await Xunit.Assert.ThrowsAsync<Exception>(() => _controller.AgregarCamposConciliacion(camposConciliacion));
            }
        }
    }


