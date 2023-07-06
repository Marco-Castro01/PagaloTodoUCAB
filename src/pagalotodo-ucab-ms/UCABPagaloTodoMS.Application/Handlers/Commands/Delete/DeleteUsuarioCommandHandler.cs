using FluentValidation;
using GreenPipes;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador del comando para eliminar un servicio de prueba.
    /// </summary>
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, string>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<DeleteUsuarioCommandHandler> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase DeleteServicioPruebaCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos de UCABPagaloTodo.</param>
        /// <param name="logger">Logger para el manejo de registros.</param>
        public DeleteUsuarioCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<DeleteUsuarioCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja la ejecución del comando para eliminar un servicio de prueba.
        /// </summary>
        /// <param name="request">Comando de eliminación de servicio de prueba.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>El identificador del servicio eliminado.</returns>
        public async Task<string> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("EliminarServicioPruebaCommandHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        private async Task<string> HandleAsync(DeleteUsuarioCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("EliminarServicioPruebaCommandHandler.HandleAsync {Request}", request);
                var entity = UsuariosMapper.MapRequestDeleteEntity(request,_dbContext);
                if (entity == null)
                    throw new CustomException(404, "Usuario no envontrado");
                _dbContext.Usuarios.Update(entity);
                _dbContext.DbContext.SaveChanges();

                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("EliminarServicioPruebaCommandHandler.HandleAsync {Response}", id);
                return "Eliminacion exitosa";
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error EliminarServicioPruebaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException("Error al eliminar Servicio", ex);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error EliminarServicioPruebaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);

            }
        }
    }
}
