using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class GetDeudaInfoQueryHandler : IRequestHandler<GetDeudaInfoQuery, DeudaResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<GetDeudaInfoQueryHandler> _logger;
        private readonly IMediator _mediator;

        public GetDeudaInfoQueryHandler(IUCABPagaloTodoDbContext dbContext, IMediator mediator, ILogger<GetDeudaInfoQueryHandler> logger)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<DeudaResponse> Handle(GetDeudaInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarDeudaQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync(request);
            }
            catch (ArgumentNullException ex)
            {
                throw new CustomException("RequestNulo", ex);
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("ConsultarDeudaQueryHandler.Handle: ArgumentNullException");
                throw new CustomException(ex.Message);
            }
        }

        private async Task<DeudaResponse> HandleAsync(GetDeudaInfoQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarPagoQueryHandler.HandleAsync");


                var deuda = await _dbContext.Deuda.Include(x => x.servicio).FirstOrDefaultAsync(x=>x.Id==request._idDeuda);
                var deudaResponde = new DeudaResponse()
                {
                    idDeuda = deuda.Id,
                    identificador = deuda.identificador,
                    servicioId = deuda.servicio.Id,
                    servicioName = deuda.servicio.name,
                    deuda = deuda.deuda,
                    camposPagos = JsonConvert.DeserializeObject<List<CamposPagosRequest>>(deuda.servicio.formatoDePagos)

                };
                    


                // Consulta los registros de la tabla Deuda que coincidan con el identificador y deudaStatus especificados en la consulta


                // Ejecuta la consulta y devuelve los resultados como una lista
                return deudaResponde;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPagoQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException("Error en consulta", ex);
            }
        }




    }
}
