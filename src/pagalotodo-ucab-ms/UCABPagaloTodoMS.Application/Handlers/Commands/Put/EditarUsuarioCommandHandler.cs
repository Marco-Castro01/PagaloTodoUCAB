using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class EditarUsuarioCommandHandler : IRequestHandler<EditarUsuarioCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<EditarUsuarioCommandHandler> _logger;

        public EditarUsuarioCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<EditarUsuarioCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Guid> Handle(EditarUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("EditarUsuarioCommand.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Guid> HandleAsync(EditarUsuarioCommand request)
        {
            using (var transaccion = _dbContext.BeginTransaction())
            {
                try
                {
                    _logger.LogInformation("EditarUsuarioCommand.HandleAsync {Request}", request);

                    // Buscar el usuario que se va a editar
                    var user = _dbContext.Usuarios.SingleOrDefault(u => u.Id == request._id);

                    if (user == null)
                    {
                        throw new CustomException($"No se encontró el usuario con ID {request._id}.");
                    }

                    if (user is PrestadorServicioEntity prestador)
                    {
                        // El usuario es un prestador de servicio
                        prestador.rif = request._request.rif;
                        // Actualizar los datos del usuario con los valores del comando
                        prestador.status = request._request.status;
                        prestador.name = request._request.name;
                        prestador.nickName = request._request.nickName;

                        _dbContext.Usuarios.Update(prestador);
                        await _dbContext.SaveEfContextChanges("APP");

                    }
                    else
                    {
                        // Actualizar los datos del usuario con los valores del comando
                        user.status = request._request.status;
                        user.name = request._request.name;
                        user.nickName = request._request.nickName;
                        user.cedula = request._request.cedula;

                        _dbContext.Usuarios.Update(user);
                        await _dbContext.SaveEfContextChanges("APP");

                    }




                    var id = user.Id;
                    transaccion.Commit();
                    _logger.LogInformation("EditarUsuarioCommand.HandleAsync {Response}", id);
                    return id;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error EditarUsuarioCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                    transaccion.Rollback();
                    throw new CustomException(ex.Message);
                }
            }
        }
    }
}
