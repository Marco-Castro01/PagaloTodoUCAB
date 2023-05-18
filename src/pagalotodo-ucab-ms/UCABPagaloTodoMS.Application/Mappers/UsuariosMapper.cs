using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
    public class UsuariosMapper
    {
        public static UsuariosResponse MapEntityAResponse(UsuarioEntity entity)
        {
            var response = new UsuariosResponse()
            {
                Id = entity.Id,
            };
            return response;
        }

        // SE CREA EL HASH DE CLAVE DEL USUSARIO
        public static UsuarioEntity MapRequestEntity(UsuarioRequest request)
        {
                if (request.tipou == 1)
                {
                 var u = new PrestadorServicioEntity();
                    using (var hash = new HMACSHA512())
                    {
                        u.email = request.email;
                    u.cedula = request.cedula;
                    u.nickName = request.nickName;
                    u.name = request.name;
                   
                        u.passwordSalt = hash.Key;
                        u.passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password));
                    return u;
                }

                }
            return null;


        }
         }
}
