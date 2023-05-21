using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarPagoQueryHandler : IRequestHandler<ConsultarPagoPruebaQuery, List<PagoResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarPagoQueryHandler> _logger;

        public ConsultarPagoQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarPagoQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<PagoResponse>> Handle(ConsultarPagoPruebaQuery request, CancellationToken cancellationToken)
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
                    return HandleAsync();
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarPagoQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<List<PagoResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarPagoQueryHandler.HandleAsync");

                var result = _dbContext.Pago.Select(c => new PagoResponse()
                {
                    Id = c.Id,
                    valor = c.valor,
                    consumidorId = c.consumidor.Id,
                    servicioId = c.servicio.Id,
                    PrestadorServicioNombre = c.servicio.PrestadorServicio.name,
                    NombreServicio = c.servicio.name,
                    NombreConsumidor = c.consumidor.name
                    

                });

                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPagoQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }
        }
    }
}