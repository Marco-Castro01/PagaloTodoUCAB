/*using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Tests.DataSeed;
using Xunit;
using UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries.ConsumidorQueryTests.UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries.ConsumidorQueryTests.UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries.ConsumidorQueryTests.UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries.ConsumidorQueryTests.UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries.ConsumidorQueryTests.UCABPagaloTodoMS.Core.Mapping;
using UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries.ConsumidorQueryTests.UCABPagaloTodoMS.Core.Entities;


    namespace UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries.ConsumidorQueryTests
    {
        public class CrearUsuarioQueryHandlerTests
        {
            private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
            private readonly IMapper _mapper;
            private readonly CrearConsumidorQueryHandler _handler;

            public CrearUsuarioQueryHandlerTests()
            {
                _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
                _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
                _handler = new CrearUsuarioQueryHandler(_dbContextMock.Object, _mapper);
            }

            [Fact]
            public async Task Handle_ReturnsUsuarioResponse()
            {
                // Arrange
                var request = new CrearUsuarioPruebaQuery { Nombre = "Juan", Apellido = "Pérez", CorreoElectronico = "juan.perez@example.com", Clave = "password" };
                _dbContextMock.Setup(m => m.Usuarios.AddAsync(It.IsAny<UsuarioEntity>(), default)).Returns(Task.CompletedTask);
                _dbContextMock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

                // Act
                var result = await _handler.Handle(request, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<UsuarioResponse>(result);
                Assert.Equal(request.Nombre, result.Nombre);
                Assert.Equal(request.Apellido, result.Apellido);
                Assert.Equal(request.CorreoElectronico, result.CorreoElectronico);
            }

            [Fact]
            public async Task Handle_ThrowsArgumentException()
            {
                // Arrange
                var request = new CrearUsuarioPruebaQuery { Nombre = "Juan", Apellido = "Pérez", CorreoElectronico = "juan.perez@example.com", Clave = "password" };
                _dbContextMock.Setup(m => m.Usuarios.AddAsync(It.IsAny<UsuarioEntity>(), default)).Throws(new ArgumentException());

                // Act
                var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(request, CancellationToken.None));

                // Assert
                Assert.NotNull(ex);
            }
        }
    }
*/