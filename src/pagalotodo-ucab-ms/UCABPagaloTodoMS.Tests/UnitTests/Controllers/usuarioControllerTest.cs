﻿using Bogus;
using FluentValidation.Internal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Azure.Amqp.Transaction;
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
    public class usuarioControllerTest
    {
        private readonly usuarioController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<usuarioController>> _loggerMock;


        public usuarioControllerTest()
        {
            _loggerMock = new Mock<ILogger<usuarioController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new usuarioController(_loggerMock.Object, _mediatorMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();

        }





        [Fact(DisplayName = "Usuario Good")]
        public async Task Login_ReturnsOkResponse_WhenCredentialsAreValid()
        {
            // Arrange
            var usuario = new LoginRequest { /* initialize properties */ };
            var expectedResponse = new UsuariosResponse { /* initialize properties */ };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginQuery>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.login(usuario);

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Xunit.Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact(DisplayName = "Usuario Bad")]
        public async Task Login_ThrowsException_WhenMediatorThrowsException()
        {
            // Arrange
            var usuario = new LoginRequest { /* initialize properties */ };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginQuery>(), default))
                .ThrowsAsync(new Exception());

            //Continuando con la prueba unitaria del método `login`:


            // Act and Assert
            await Xunit.Assert.ThrowsAsync<Exception>(() => _controller.login(usuario));
        }
    }
}