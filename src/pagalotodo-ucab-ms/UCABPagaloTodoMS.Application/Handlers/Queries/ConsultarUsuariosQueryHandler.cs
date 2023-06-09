using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    internal class ConsultarUsuariosQueryHandler : IRequestHandler<ConsultarUsuariosQuery, List<UsuariosAllResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarUsuariosQueryHandler> _logger;

        public ConsultarUsuariosQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarUsuariosQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<UsuariosAllResponse>> Handle(ConsultarUsuariosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarUsuariosQueryHandler.Handle: Request nulo.");
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
                _logger.LogWarning("ConsultarUsuariosQueryHandler.Handle: ArgumentNullException");
                throw new CustomException(ex.Message);
            }
        }

        private async Task<List<UsuariosAllResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarUsuariosQueryHandler.HandleAsync");

                // Consulta todos los registros de la tabla Usuarios
                var result = _dbContext.Usuarios.Select(c => new UsuariosAllResponse()
                {
                    Id = c.Id,
                    email = c.email,
                    name = c.name,
                    nickName = c.nickName,
                    status = c.status,
                    Discriminator = c.Discriminator
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException("Error en consulta", ex);
            }
        }
    }
}
