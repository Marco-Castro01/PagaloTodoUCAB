using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class AgregarPrestadorCommandHandler : IRequestHandler<AgregarPrestadorCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarPrestadorCommandHandler> _logger;
        public AgregarPrestadorCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AgregarPrestadorCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public async Task<Guid> Handle(AgregarPrestadorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<Guid> HandleAsync(AgregarPrestadorCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarUsuarioCommand.HandleAsync {Request}", request);
                var entity = UsuariosMapper.MapRequestPrestadorEntity(request._request);
                PrestadorValidator usuarioValidator = new PrestadorValidator();
                ValidationResult result = usuarioValidator.Validate(entity);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);

                }
                _dbContext.Usuarios.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarAdminHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarAdminHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);

            }
        }
    }
}



