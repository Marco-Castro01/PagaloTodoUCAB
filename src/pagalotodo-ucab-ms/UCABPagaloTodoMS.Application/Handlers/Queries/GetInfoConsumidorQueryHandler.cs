using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class GetInfoConsumidorQueryHandler : IRequestHandler<GetInfoConsumidorQuery, ConsumidorResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<GetInfoConsumidorQueryHandler> _logger;

        public GetInfoConsumidorQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<GetInfoConsumidorQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<ConsumidorResponse> Handle(GetInfoConsumidorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarConsumidorQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync(request);
            }
            catch (ArgumentNullException ex)
            {
                throw new CustomException("Request nulo", ex);
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarPrestadorServicioQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<ConsumidorResponse> HandleAsync(GetInfoConsumidorQuery reques)
        {
            try
            {
                _logger.LogInformation("ConsultarConsumidorQueryHandler.HandleAsync");

                // Consulta los registros de la tabla Consumidor y los mapea a objetos ConsumidorResponse
                var result = _dbContext.Consumidor
                    .FirstOrDefault(x => x.deleted == false && x.Id == reques._idConsumidor);
                if (result == null)
                    throw new CustomException(404, "Consumidor no existente");

                ConsumidorResponse consumidor = new ConsumidorResponse()
                {
                    Id = result.Id,
                    cedula = result.cedula,
                    nickName = result.nickName,
                    status = result.status,
                    email = result.email,

                };
                // Ejecuta la consulta y devuelve los resultados como una lista
                return consumidor;
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPrestadorServicioQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException("Error",ex);
            }
        }
    }
}
