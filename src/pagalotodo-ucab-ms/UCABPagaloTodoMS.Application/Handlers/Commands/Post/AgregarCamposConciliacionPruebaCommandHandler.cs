using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para agregar campos de conciliación de prueba.
    /// </summary>
    public class AgregarCamposConciliacionPruebaCommandHandler : IRequestHandler<AgregarCamposConciliacionPruebaCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarCamposConciliacionPruebaCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AgregarCamposConciliacionPruebaCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public AgregarCamposConciliacionPruebaCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AgregarCamposConciliacionPruebaCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja el comando para agregar campos de conciliación de prueba.
        /// </summary>
        /// <param name="request">Comando para agregar campos de conciliación de prueba</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador de los campos de conciliación agregados</returns>
        public async Task<Guid> Handle(AgregarCamposConciliacionPruebaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("AgregarCamposConciliacionPruebaCommandHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return await HandleAsync(request);
            }
            catch (CustomException)
            {
                throw;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Maneja asincrónicamente el comando para agregar campos de conciliación de prueba.
        /// </summary>
        /// <param name="request">Comando para agregar campos de conciliación de prueba</param>
        /// <returns>Identificador de los campos de conciliación agregados</returns>
        private async Task<Guid> HandleAsync(AgregarCamposConciliacionPruebaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarCamposConciliacionPruebaCommandHandler.HandleAsync {Request}", request);
                var entity = CamposConciliacionMapper.MapRequestEntity(request._request);
                CamposConciliacionValidator campoConciliacionValidator = new CamposConciliacionValidator();
                ValidationResult result = await campoConciliacionValidator.ValidateAsync(entity);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
                _dbContext.CamposConciliacion.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarCamposConciliacionPruebaCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error AgregarCamposConciliacionPruebaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException("Error al crear Campo de conciliacion", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarCamposConciliacionPruebaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);
            }
        }
    }
}
