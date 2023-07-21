/*using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Commands.Patch;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using Xunit;

namespace UCABPagaloTodoMS.Application.Tests.Handlers.Commands.Patch
{
    public class UpdateServicioPruebaCommandHandlerTests
    {
        private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
        private readonly Mock<ILogger<UpdateServicioPruebaCommandHandler>> _loggerMock;
        private readonly UpdateServicioPruebaCommandHandler _handler;

        public UpdateServicioPruebaCommandHandlerTests()
        {
            _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
            _loggerMock = new Mock<ILogger<UpdateServicioPruebaCommandHandler>>();
            _handler = new UpdateServicioPruebaCommandHandler(_dbContextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldReturnSuccessMessage()
        {
            // Arrange
            var command = new UpdateServicioPruebaCommand
            {
                Request = new ServicioRequest
                {
                    Nombre = "Nuevo nombre de servicio",
                    Descripcion = "Nueva descripción de servicio",
                    Precio = 50
                }
            };
            var entity = new ServicioEntity
            {
                Id = Guid.NewGuid(),
                Nombre = command.Request.Nombre,
                Descripcion = command.Request.Descripcion,
                Precio = command.Request.Precio
            };
            var validationResult = new ValidationResult();
            _dbContextMock.Setup(x => x.BeginTransaction()).Returns(new Mock<IDbContextTransaction>().Object);
            _dbContextMock.Setup(x => x.Servicio.Update(entity));
            _dbContextMock.Setup(x => x.DbContext.SaveChanges());
            _dbContextMock.Setup(x => x.SaveEfContextChanges("APP")).Returns(Task.CompletedTask);
            _dbContextMock.Setup(x => x.Servicio.GetByIdAsync(entity.Id)).ReturnsAsync(entity);
            _loggerMock.Setup(x => x.Log(LogLevel.Information, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
            var servicioValidatorMock = new Mock<IValidator<ServicioEntity>>();
            servicioValidatorMock.Setup(x => x.ValidateAsync(entity, CancellationToken.None)).ReturnsAsync(validationResult);
            _handler = new UpdateServicioPruebaCommandHandler(_dbContextMock.Object, _loggerMock.Object);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("Eliminacion exitosa", result);
        }

        [Fact]
        public async Task Handle_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            UpdateServicioPruebaCommand command = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithValidationException_ShouldThrowValidationException()
        {
            // Arrange
            var command = new UpdateServicioPruebaCommand
            {
                Request = new ServicioRequest
                {
                    Nombre = "Nuevo nombre de servicio",
                    Descripcion = "Nueva descripción de servicio",
                    Precio = -50 // Precio inválido
                }
            };
            var entity = new ServicioEntity
            {
                Id = Guid.NewGuid(),
                Nombre = command.Request.Nombre,
                Descripcion = command.Request.Descripcion,
                Precio = command.Request.Precio
            };
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Precio", "El precio debe ser mayor que cero.") });
            _dbContextMock.Setup(x => x.BeginTransaction()).Returns(new Mock<IDbContextTransaction>().Object);
            _dbContextMock.Setup(x => x.Servicio.Update(entity));
            _dbContextMock.Setup(x => x.DbContext.SaveChanges());
            _dbContextMock.Setup(x => x.SaveEfContextChanges("APP")).Returns(Task.CompletedTask);
            _dbContextMock.Setup(x => x.Servicio.GetByIdAsync(entity.Id)).ReturnsAsync(entity);
            _loggerMock.Setup(x => x.Log(LogLevel.Information, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
            var servicioValidatorMock = new Mock<IValidator<ServicioEntity>>();
            servicioValidatorMock.Setup(x => x.ValidateAsync(entity, CancellationToken.None)).ReturnsAsync(validationResult);
            _handler = new UpdateServicioPruebaCommandHandler(_dbContextMock.Object, _loggerMock.Object);

            // Act & Assert
            awaitAssert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithDbUpdateException_ShouldThrowDbUpdateException()
        {
            // Arrange
            var command = new UpdateServicioPruebaCommand
            {
                Request = new ServicioRequest
                {
                    Nombre = "Nuevo nombre de servicio",
                    Descripcion = "Nueva descripción de servicio",
                    Precio = 50
                }
            };
            var entity = new ServicioEntity
            {
                Id = Guid.NewGuid(),
                Nombre = command.Request.Nombre,
                Descripcion = command.Request.Descripcion,
                Precio = command.Request.Precio
            };
            _dbContextMock.Setup(x => x.BeginTransaction()).Returns(new Mock<IDbContextTransaction>().Object);
            _dbContextMock.Setup(x => x.Servicio.Update(entity));
            _dbContextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new Exception("Error al actualizar servicio."));
            _dbContextMock.Setup(x => x.RollbackTransaction());
            _loggerMock.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
            var servicioValidatorMock = new Mock<IValidator<ServicioEntity>>();
            servicioValidatorMock.Setup(x => x.ValidateAsync(entity, CancellationToken.None)).ReturnsAsync(new ValidationResult());
            _handler = new UpdateServicioPruebaCommandHandler(_dbContextMock.Object, _loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithCustomException_ShouldThrowCustomException()
        {
            // Arrange
            var command = new UpdateServicioPruebaCommand
            {
                Request = new ServicioRequest
                {
                    Nombre = "Nuevo nombre de servicio",
                    Descripcion = "Nueva descripción de servicio",
                    Precio = 50
                }
            };
            var entity = new ServicioEntity
            {
                Id = Guid.NewGuid(),
                Nombre = command.Request.Nombre,
                Descripcion = command.Request.Descripcion,
                Precio = command.Request.Precio
            };
            _dbContextMock.Setup(x => x.BeginTransaction()).Returns(new Mock<IDbContextTransaction>().Object);
            _dbContextMock.Setup(x => x.Servicio.Update(entity));
            _dbContextMock.Setup(x => x.DbContext.SaveChanges());
            _dbContextMock.Setup(x => x.SaveEfContextChanges("APP")).Throws(new CustomException("Error al guardar cambios en el contexto."));
            _dbContextMock.Setup(x => x.RollbackTransaction());
            _loggerMock.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
            var servicioValidatorMock = new Mock<IValidator<ServicioEntity>>();
            servicioValidatorMock.Setup(x => x.ValidateAsync(entity, CancellationToken.None)).ReturnsAsync(new ValidationResult());
            _handler = new UpdateServicioPruebaCommandHandler(_dbContextMock.Object, _loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}*/