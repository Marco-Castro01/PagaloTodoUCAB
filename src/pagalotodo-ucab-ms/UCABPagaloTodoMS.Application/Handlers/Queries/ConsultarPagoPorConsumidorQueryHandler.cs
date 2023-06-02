using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarPagoPorConsumidorQueryHandler : IRequestHandler<ConsultarPagoPorConsumidorQuery, List<PagoResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarPagoPorConsumidorQueryHandler> _logger;

        public ConsultarPagoPorConsumidorQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarPagoPorConsumidorQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<PagoResponse>> Handle(ConsultarPagoPorConsumidorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarPagoQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("ConsultarPagoQueryHandler.Handle: ArgumentNullException");
                throw new CustomException(ex.Message);
            }
        }

        private async Task<List<PagoResponse>> HandleAsync(ConsultarPagoPorConsumidorQuery  request)
        {
            try
            {
                _logger.LogInformation("ConsultarPagoQueryHandler.HandleAsync");
                
                var result = _dbContext.Pago.Where(c => c.consumidor.Id == request.IdConsumidor).Select(c => new PagoResponse()
                {
                    Id = c.Id,
                    valor = c.valor,
                    consumidorId = c.consumidor.Id,
                    servicioId = c.servicio.Id,
                    PrestadorServicioNombre = c.servicio.PrestadorServicio.name,
                    NombreServicio = c.servicio.name,
                    NombreConsumidor = c.consumidor.name
                    

                });

                return await result.ToListAsync() ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPagoQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException(ex.Message);
            }
        }
    }
}