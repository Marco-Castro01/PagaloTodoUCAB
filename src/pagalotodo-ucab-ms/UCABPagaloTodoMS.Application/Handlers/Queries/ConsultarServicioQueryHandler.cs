using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;
using Newtonsoft.Json;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarServicioQueryHandler : IRequestHandler<ConsultarServicioPruebaQuery, List<ServicioResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarServicioQueryHandler> _logger;

        public ConsultarServicioQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarServicioQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<ServicioResponse>> Handle(ConsultarServicioPruebaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarServicioQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw new CustomException("Request Nulo", ex);
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("ConsultarServicioQueryHandler.Handle: ArgumentNullException");
                throw new CustomException(ex.Message);
            }
        }

        private async Task<List<ServicioResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarServicioQueryHandler.HandleAsync");

                // Consulta todos los registros de la tabla Servicio donde deleted es falso
                var result = _dbContext.Servicio.Include(x => x.ServicioCampo)
            .Where(c => c.deleted == false)
            .Select(c => new ServicioResponse
            {
                Id = c.Id,
                name = c.name,
                accountNumber = c.accountNumber,
                prestadorServicioId = c.PrestadorServicio.Id,
                prestadorServicioName = c.PrestadorServicio.name,
                CamposDeLosPagos = JsonConvert.DeserializeObject<List<CamposPagosRequest>>(c.formatoDePagos),
                statusServicio = c.statusServicio,
                tipoServicio = c.tipoServicio,
                CamposConciliacion = c.ServicioCampo
                    .Where(sc => sc.ServicioId == c.Id && sc.Campo.deleted==false)
                    .Select(sc => new CamposConciliacionResponse
                    {
                        Id = sc.Campo.Id,
                        Nombre = sc.Campo.Nombre,
                        Longitud = sc.Campo.Longitud
                    })
                    .ToList()
            })
            .ToList();



                // Ejecuta la consulta y devuelve los resultados como una lista
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarServicioQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException("Error en consulta", ex);
            }
        }
    }
}
