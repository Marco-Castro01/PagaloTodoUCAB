using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries.CamposConciliacionQueryTests
{
    public class ConsultarCamposConciliacionQueryHandlerTests
    {
        private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
        private readonly Mock<ILogger<ConsultarCamposConciliacionQueryHandler>> _loggerMock;
        private readonly ConsultarCamposConciliacionQueryHandler _handler;

        public ConsultarCamposConciliacionQueryHandlerTests()
        {
            _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
            _loggerMock = new Mock<ILogger<ConsultarCamposConciliacionQueryHandler>>();
            _handler = new ConsultarCamposConciliacionQueryHandler(_dbContextMock.Object, _loggerMock.Object);
        }

        [Fact(DisplayName = "")]
        public async Task Handle_ReturnsListOfCamposConciliacionResponse()
        {

            var response = new List<CamposConciliacionEntity>()
        {
            new CamposConciliacionEntity() { Nombre = "Campo1", Longitud = 10 },
            new CamposConciliacionEntity() {  Nombre = "Campo2", Longitud = 20 }
        };
            _dbContextMock.Setup(m => m.CamposConciliacion).Returns(MockDbSet(response));

            var result = await _handler.Handle(new ConsultarCamposConciliacionPruebaQuery(), CancellationToken.None);


            Assert.IsType<List<CamposConciliacionResponse>>(result);
            Assert.Equal(response.Count, result.Count);
        }

        [Fact(DisplayName = "")]
        public async Task Handle_ThrowsArgumentNullException()
        {



            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));


            Assert.NotNull(ex);
        }

        private static DbSet<T> MockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());
            return mockSet.Object;
        }
    }
}