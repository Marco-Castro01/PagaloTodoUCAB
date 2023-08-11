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
    public class GetInfoPrestadorServicioQueryHandler : IRequestHandler<GetInfoPrestadorServicioQuery, PrestadorServicioResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<GetInfoPrestadorServicioQueryHandler> _logger;

        public GetInfoPrestadorServicioQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<GetInfoPrestadorServicioQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<PrestadorServicioResponse> Handle(GetInfoPrestadorServicioQuery request, CancellationToken cancellationToken)
        {
            try

            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarPrestadorServicioQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync(request);
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
                _logger.LogWarning("ConsultarPrestadorServicioQueryHandler.Handle: ArgumentNullException");
                throw new CustomException(ex.Message);
            }
        }

        private async Task<PrestadorServicioResponse> HandleAsync(GetInfoPrestadorServicioQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarPrestadorServicioQueryHandler.HandleAsync");

                // Consulta todos los registros de la tabla PrestadorServicio

                var result = _dbContext.PrestadorServicio.Include(x=>x.Servicio)
                   .FirstOrDefault(x => x.deleted == false && x.Id == request._idPrestador);
                
                if (result == null)
                    throw new CustomException(404, "Consumidor no existente");

                PrestadorServicioResponse prestador = new PrestadorServicioResponse()
                {
                    Id = result.Id,
                    rif = result.rif,
                    nickName = result.nickName,
                    
                    status = result.status,
                    email = result.email,

                };
                // Ejecuta la consulta y devuelve los resultados como una lista
                return prestador;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPrestadorServicioQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException(ex.Message);
            }
        }
    }
}
