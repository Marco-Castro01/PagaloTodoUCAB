using MediatR;
using Microsoft.Extensions.Logging;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para asignar un campo de conciliación.
    /// </summary>
    public class AsignarCampoCommandHandler : IRequestHandler<AsignarCamposCommand, List<ServicioCampoEntity>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AsignarCampoCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AsignarCampoCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public AsignarCampoCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AsignarCampoCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja el comando para asignar un campo de conciliación.
        /// </summary>
        /// <param name="request">Comando para asignar un campo de conciliación</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador del campo de conciliación asignado</returns>
        public async Task<List<ServicioCampoEntity>> Handle(AsignarCamposCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return await HandleAsync(request);
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
                throw;
            }
        }

        /// <summary>
        /// Maneja asincrónicamente el comando para asignar un campo de conciliación.
        /// </summary>
        /// <param name="request">Comando para asignar un campo de conciliación</param>
        /// <returns>Identificador del campo de conciliación asignado</returns>
        private async Task<List<ServicioCampoEntity>> HandleAsync(AsignarCamposCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                
                var servicio = await _dbContext.Servicio.FindAsync(request._servicioId);
                if (servicio == null)
                    throw new CustomException(422, "Error al buscar servicio");

                List<CamposConciliacionEntity> campos=await _dbContext.CamposConciliacion.Where(c => request._request.Id.Contains(c.Id))
                    .ToListAsync();
                List<ServicioCampoEntity> servicioCampo = new List<ServicioCampoEntity>();
                foreach (var campo in campos)
                {
                    servicioCampo.Add(new ServicioCampoEntity()
                    {
                        ServicioId = servicio.Id,
                        CampoId = campo.Id,
                        Servicio = servicio,
                        Campo = campo
                        
                    });
                    
                }
                _dbContext.ServicioCampo.AddRange(servicioCampo);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                var id = servicio.Id;

                _logger.LogInformation("AsignarCampoCommandHandler.HandleAsync {Response}", id);
                
                return servicioCampo;
                
            }catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error AsignarCampoCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw CustomException.CrearDesdeListaException(422, "Se produjeron los siguientes errores de validación",ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AsignarCampoCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }
}