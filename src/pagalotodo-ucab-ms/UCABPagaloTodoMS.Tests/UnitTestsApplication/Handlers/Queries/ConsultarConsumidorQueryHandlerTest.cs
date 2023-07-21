/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using Xunit;

namespace UCABPagaloTodoMS.Application.UnitTests.Handlers.Queries
{
    public class ConsultarConsumidorQueryHandlerTests
    {
        private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
        private readonly Mock<ILogger<ConsultarConsumidorQueryHandler>> _loggerMock;

        public ConsultarConsumidorQueryHandlerTests()
        {
            _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
            _loggerMock = new Mock<ILogger<ConsultarConsumidorQueryHandler>>();
        }

        [Fact]
        public async Task Handle_ReturnsListOfConsumidorResponse_WhenRequestIsNotNull()
        {
            // Arrange
            var handler = new ConsultarConsumidorQueryHandler(_dbContextMock.Object, _loggerMock.Object);
            var query = new ConsultarConsumidorPruebaQuery();
            var consumidores = new List<ConsumidorEntity>
            {
                new ConsumidorEntity
                {
                    Id = Guid,
                    cedula = "12345678",
                    nickName = "usuario1",
                    status = true,
                    email = "usuario1@ucab.edu.ve",
                },
                new ConsumidorEntity
                {
                    Id = 2,
                    cedula = "23456789",
                    nickName = "usuario2",
                    status = false,
                    email = "usuario2@ucab.edu.ve",
                },
            };
            var expected = consumidores.Select(c => new ConsumidorResponse()
            {
                Id = c.Id,
                cedula = c.cedula,
                nickName = c.nickName,
                status = c.status,
                email = c.email,
            }).ToList();

            _dbContextMock.Setup(db => db.Consumidor).Returns(MockHelper.GetDbSetMock(consumidores).Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ConsumidorResponse>>(result);
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected.First().Id, result.First().Id);
            Assert.Equal(expected.First().cedula, result.First().cedula);
            Assert.Equal(expected.First().nickName, result.First().nickName);
            Assert.Equal(expected.First().status, result.First().status);
            Assert.Equal(expected.First().email, result.First().email);
            Assert.Equal(expected.Last().Id, result.Last().Id);
            Assert.Equal(expected.Last().cedula, result.Last().cedula);
            Assert.Equal(expected.Last().nickName, result.Last().nickName);
            Assert.Equal(expected.Last().status, result.Last().status);
            Assert.Equal(expected.Last().email, result.Last().email);
        }

        [Fact]
        public async Task Handle_ThrowsCustomException_WhenExceptionOccurs()
        {
            // Arrange
            var handler = new ConsultarConsumidorQueryHandler(_dbContextMock.Object, _loggerMock.Object);
            var query = new ConsultarConsumidorPruebaQuery();

            _dbContextMock.Setup(db => db.Consumidor).Throws(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsArgumentNullException_WhenRequestIsNull()
        {
            // Arrange
            var handler = new ConsultarConsumidorQueryHandler(_dbContextMock.Object, _loggerMock.Object);
            ConsultarConsumidorPruebaQuery query = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(query, CancellationToken.None));
        }
    }
}*/