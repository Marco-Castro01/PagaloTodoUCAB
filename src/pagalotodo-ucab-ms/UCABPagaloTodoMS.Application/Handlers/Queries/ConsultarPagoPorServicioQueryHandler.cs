using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarPagoPorServicioQueryHandler : IRequestHandler<ConsultarPagoPorServicioQuery, List<PagoResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarPagoPorServicioQueryHandler> _logger;

        public ConsultarPagoPorServicioQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarPagoPorServicioQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<PagoResponse>> Handle(ConsultarPagoPorServicioQuery request, CancellationToken cancellationToken)
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

        private async Task<List<PagoResponse>> HandleAsync(ConsultarPagoPorServicioQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarPagoQueryHandler.HandleAsync");

                // Consulta los registros de la tabla Pago que correspondan al servicio especificado en la consulta
                var result = _dbContext.Pago
                    .Where(c => c.servicio.Id == request.IdPrestador)
                    .Select(c => new PagoResponse()
                    {
                        Id = c.Id,
                        valor = c.valor,
                        consumidorId = c.consumidor.Id,
                        servicioId = c.servicio.Id,
                        PrestadorServicioNombre = c.servicio.PrestadorServicio.name,
                        NombreServicio = c.servicio.name,
                        NombreConsumidor = c.consumidor.name
                    });

                // Ejecuta la consulta y devuelve los resultados como una lista
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPagoQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException(ex.Message);
            }
        }
    }
}
