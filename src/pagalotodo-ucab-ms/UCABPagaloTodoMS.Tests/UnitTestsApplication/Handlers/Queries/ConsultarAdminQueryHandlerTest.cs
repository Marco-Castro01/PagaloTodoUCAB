using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Tests.DataSeed;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries.AdministradorQueryTests
{
    public class ConsultarAdminQueryHandlerTests
    {
        private readonly ConsultarAdminQueryHandler _handler;
        private readonly Mock<IUCABPagaloTodoDbContext> _contextMock;
        private readonly Mock<ILogger<ConsultarAdminQueryHandler>> _mockLogger;

        public ConsultarAdminQueryHandlerTests()
        {
            _mockLogger = new Mock<ILogger<ConsultarAdminQueryHandler>>();
            _contextMock = new Mock<IUCABPagaloTodoDbContext>();
            _handler = new ConsultarAdminQueryHandler(_contextMock.Object, _mockLogger.Object);
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName = "Método Handle para consultar administradores")]
        public async Task Handle_ConsultarAdministradores_exitosamente()
        {
            var request = new ConsultarAdminPruebaQuery();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsType<List<AdminResponse>>(result);
        }

        [Fact(DisplayName = "Método Handle para consultar administradores exception")]
        public void Handle_ConsultarAdministradores_exception()
        {


            ConsultarAdminPruebaQuery request = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}