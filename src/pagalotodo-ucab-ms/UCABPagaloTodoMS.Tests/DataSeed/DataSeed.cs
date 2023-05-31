using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Core.Database;
using MockQueryable.Moq;
using Moq;
using UCABPagaloTodoMS.Infrastructure.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Infrastructure.Migrations;


namespace UCABPagaloTodoMS.Tests.DataSeed
{
    public static class DataSeed
    {
        public static Mock<DbSet<AdminEntity>> mockSetAdminEntity = new Mock<DbSet<AdminEntity>>();
        public static Mock<DbSet<BaseEntity>> mockSetBaseEntity = new Mock<DbSet<BaseEntity>>();
        public static Mock<DbSet<CamposConciliacionEntity>> mockSetCamposConciliacionEntity = new Mock<DbSet<CamposConciliacionEntity>>();
    public static Mock<DbSet<ConsumidorEntity>> mockSetConsumidorEntity = new Mock<DbSet<ConsumidorEntity>>();
    public static Mock<DbSet<PagoEntity>> mockSetSPagoEntity = new Mock<DbSet<PagoEntity>>();
    public static Mock<DbSet<PrestadorServicioEntity>> mockSetPrestadorServicioEntity = new Mock<DbSet<PrestadorServicioEntity>>();
    public static Mock<DbSet<ServicioEntity>> mockSetServicioEntity = new Mock<DbSet<ServicioEntity>>();
    public static Mock<DbSet<UsuarioEntity>> mockSetUsuarioEntity = new Mock<DbSet<UsuarioEntity>>();
    public static Mock<DbSet<ValoresEntity>> mockSetValoresEntity = new Mock<DbSet<ValoresEntity>>();


        public static void SetupDbContextData(this Mock<IUCABPagaloTodoDbContext> mockContext)
        {

            var Admin = new List<AdminEntity>
            {
                new AdminEntity
                {
                    cedula = "123456789"
                },


            };

            var Base = new List<BaseEntity>
            {
                 new BaseEntity
                {
                   // Id = 1,
                    CreatedBy = "Bismarck",
                    UpdatedAt = DateTime.Now,
                    
                },



            };
            var CamposConciliacion = new List<CamposConciliacionEntity>
            {
                 new CamposConciliacionEntity
                {
                    Nombre = "prueba",
                    Longitud = 2,
                    //Servicio
                },



            };
            var Consumidor = new List<ConsumidorEntity>
            {
                 new ConsumidorEntity
                {
                    name = "Prueba1",
                    lastName = "Arteaga",
                    cedula = "123456789",

                },



            };
            var Pagos = new List<PagoEntity>
            {
                 new PagoEntity
                {
                    cedula = "123456789"
                },



            };
            var Prestador = new List<PrestadorServicioEntity>
            {
                 new PrestadorServicioEntity
                {
                    cedula = "123456789"
                },



            };
            var Servicio = new List<ServicioEntity>
            {
                 new ServicioEntity
                {
                    cedula = "123456789"
                },



            };
            var Usuario = new List<UsuarioEntity>
            {
                 new UsuarioEntity
                {
                    cedula = "123456789"
                },



            };
            var Valores = new List<ValoresEntity>
            {
                 new ValoresEntity
                {
                    cedula = "123456789"
                },



            };



        }
    }
