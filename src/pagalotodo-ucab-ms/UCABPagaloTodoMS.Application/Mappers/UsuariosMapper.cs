using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public class UsuariosMapper
    {

        // SE CREA EL HASH DE CLAVE DEL USUSARIO
        public static AdminEntity MapRequestAdminEntity(AdminRequest request)
        {
                         
            var u = new AdminEntity();
            using (var hash = new HMACSHA512())
            {
                u.email = request.email;
                u.cedula = request.cedula;
                u.nickName = request.nickName;
                u.name = request.name;
                u.deleted = false;
                u.status = true;
                u.passwordSalt = hash.Key;
                u.passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.password));
                return u;
            }
        }

        public static ConsumidorEntity MapRequestConsumidorEntity(ConsumidorRequest request)
        {

            var u = new ConsumidorEntity();
            using (var hash = new HMACSHA512())
            {
                u.email = request.email;
                u.cedula = request.cedula;
                u.nickName = request.nickName;
                u.name = request.name;
                u.status = true;
                u.lastName = request.lastName;
                u.deleted = false;
                

                u.passwordSalt = hash.Key;
                u.passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.password));
                return u;
            }
        }
        public static PrestadorServicioEntity MapRequestPrestadorEntity(PrestadorRequest request)
        {

            var u = new PrestadorServicioEntity();
            using (var hash = new HMACSHA512())
            {
                u.email = request.email;
                u.nickName = request.nickName;
                u.name = request.name;
                u.status = true;
                u.deleted = false;
                u.rif = request.rif;



                u.passwordSalt = hash.Key;
                u.passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.password));
                return u;
            }
        }
        
        
        public static UsuarioEntity MapRequestDeleteEntity(DeleteUsuarioCommand request, IUCABPagaloTodoDbContext DbContext)
        {

            var entity=DbContext.Usuarios.FirstOrDefault(u => u.Id == request._idUsuario && u.Discriminator=="AdminEntity" && u.deleted==false);
            return entity;        }

    }
}
