using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarConsumidorQueryHandler : IRequestHandler<ConsultarConsumidorPruebaQuery, List<ConsumidorResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarConsumidorQueryHandler> _logger;

        public ConsultarConsumidorQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarConsumidorQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<ConsumidorResponse>> Handle(ConsultarConsumidorPruebaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarConsumidorQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync();
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

        private async Task<List<ConsumidorResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarConsumidorQueryHandler.HandleAsync");

                // Consulta los registros de la tabla Consumidor y los mapea a objetos ConsumidorResponse
                var result = _dbContext.Consumidor.Where(x=>x.deleted==false).Select(c => new ConsumidorResponse()
                {
                    Id = c.Id,
                    cedula = c.cedula,
                    nickName = c.nickName,
                    status = c.status,
                    email = c.email,
                });

                // Ejecuta la consulta y devuelve los resultados como una lista
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPrestadorServicioQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException("Error",ex);
            }
        }
    }
}
