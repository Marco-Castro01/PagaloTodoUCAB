using Moq;
using Xunit;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Tests.MockData;
using UCABPagaloTodoMS.Application.Commands;
using FluentValidation.Results;
using UCABPagaloTodoMS.Application.Handlers.Commands;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries
{
    public class ConsultarAdminInformacionQueryHandlerTest
    {

        private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
        private readonly Mock<IMediator> _mediatorMock;
        private Mock<IDbContextTransactionProxy> _mockTransaccion;

        private readonly ConsultarAdminInformacionQueryHandler _handler;
        private readonly Mock<ILogger<ConsultarAdminInformacionQueryHandler>> _loggerMock;


        private Mock<IRequestHandler<GetInfoAdminQuery, AdminResponse>> _mockHandler;

        public ConsultarAdminInformacionQueryHandlerTest()
        {
            _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
            _mediatorMock = new Mock<IMediator>();
            _mockTransaccion = new Mock<IDbContextTransactionProxy>();

            _loggerMock = new Mock<ILogger<ConsultarAdminInformacionQueryHandler>>();
            _handler = new ConsultarAdminInformacionQueryHandler(_dbContextMock.Object, _loggerMock.Object);



            _mockHandler = new Mock<IRequestHandler<GetInfoAdminQuery, AdminResponse>>();

            DataSeed.DataSeed.SetupDbContextData(_dbContextMock);

        }

        [Fact(DisplayName = "Handler 1")]
        public async Task Handle_1()
        {
            // Arrange
            var request = BuildDataContextFaker.AdminRequestOK();
            var response = BuildDataContextFaker.AdminResponseOK();
            var query = new GetInfoAdminQuery(request);
            var cancellationToken = new CancellationToken();

            _mockHandler.Setup(x => x.Handle(query, cancellationToken))
                       .ReturnsAsync(response);
            // Act
            var result = await _mockHandler.Object.Handle(query, cancellationToken);

            // Assert
            Xunit.Assert.NotNull(result);
        }

    }
}
