using MediatR;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UsuariosResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarValoresQueryHandler> _logger;

        public LoginQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarValoresQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<UsuariosResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("LoginQuery.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarValoresQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<UsuariosResponse> HandleAsync(LoginQuery usuario)
        {
            try
            {
                _logger.LogInformation("LoginQuery.HandleAsync");
                var usuariocreated = _dbContext.Usuarios.Where(c => c.email == usuario._request.email).FirstOrDefault();
                if (usuariocreated == null)
                {
                    _logger.LogWarning("Ha ocurrido un error el Usuario no existe");
                    throw new ArgumentNullException(nameof(usuariocreated));
                }
                if (!VerifyPasswordHash(usuario._request.Password!, usuariocreated.passwordHash, usuariocreated.passwordSalt))
                {

                    _logger.LogWarning("Ha ocurrido un error Contrasena incorrecta");
                    throw new ArgumentNullException(nameof(usuariocreated));
                }
            
                var result = _dbContext.Usuarios.Where(u => u.email == usuario._request.email)
                .Select(a => new UsuariosResponse
                {
                    Id = a.Id,
                    Discriminator = a.Discriminator
                }
                    );
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Login.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
                         using (var hash = new HMACSHA512(passwordSalt))
                {
                    var ComputedHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return ComputedHash.SequenceEqual(passwordHash);
                }
  
        }



    }
}



      

