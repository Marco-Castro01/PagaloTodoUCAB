﻿using MediatR;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, String>
    {
        private readonly IConfiguration _configuration;
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarValoresQueryHandler> _logger;

        public LoginQueryHandler(IConfiguration configuration,  IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarValoresQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _configuration = configuration;
        }

        public Task<String> Handle(LoginQuery request, CancellationToken cancellationToken)
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

        private async Task<String> HandleAsync(LoginQuery usuario)
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
                .Select(a => new UsuariosAllResponse
                {
                    Id = a.Id,
                    Discriminator = a.Discriminator,
                    email = a.email,
                    name = a.name, 
                    nickName = a.nickName,
                    status = a.status

                }
                    ).First();

                return Generate(result);
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

        private string Generate(UsuariosAllResponse user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los claims
            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.nickName),
                new Claim(ClaimTypes.GivenName, user.name),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, user.Discriminator),
            };


            // Crear el token

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}



      

