using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace UCABPagaloTodoMS.Application.Handlers.Commands.Patch
{
    /// <summary>
    /// Clase que maneja el comando para actualizar un servicio de prueba.
    /// </summary>
    public class UpdateServicioPruebaCommandHandler : IRequestHandler<UpdateServicioPruebaCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<UpdateServicioPruebaCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase UpdateServicioPruebaCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public UpdateServicioPruebaCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<UpdateServicioPruebaCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja el comando para actualizar un servicio de prueba.
        /// </summary>
        /// <param name="request">Comando para actualizar un servicio de prueba</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador del servicio de prueba actualizado</returns>
        public async Task<Guid> Handle(UpdateServicioPruebaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("AgregarServicioPruebaCommandHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Maneja asincrónicamente el comando para actualizar un servicio de prueba.
        /// </summary>
        /// <param name="request">Comando para actualizar un servicio de prueba</param>
        /// <returns>Identificador del servicio de prueba actualizado</returns>
        private async Task<Guid> HandleAsync(UpdateServicioPruebaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarServicioPruebaCommandHandler.HandleAsync {Request}" , request);
                var entity = ServicioMapper.MapRequestUpdateEntity(request._request,_dbContext);
                ServicioValidator ServicioValidator = new ServicioValidator();
                ValidationResult result = await ServicioValidator.ValidateAsync(entity);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                _dbContext.Servicio.Update(entity);
                _dbContext.DbContext.SaveChanges();

                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarServicioPruebaCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarServicioPruebaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);
            }
        }
    }
}
