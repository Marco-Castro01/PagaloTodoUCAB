using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Core.Database
{
    public interface IUCABPagaloTodoDbContext
    {
        DbSet<ValoresEntity> Valores { get;}
        DbSet<UsuarioEntity> Usuarios { get; }
        DbSet<AdminEntity> Admin { get; }
        DbSet<ConsumidorEntity> Consumidor { get; }
        DbSet<PagoEntity> Pago { get; }
        DbSet<PrestadorServicioEntity> PrestadorServicio { get; }
        DbSet<ServicioEntity> Servicio { get; }
        DbSet<DeudaEntity> Deuda { get; }
        DbSet<CamposConciliacionEntity> CamposConciliacion { get; }

        DbContext DbContext
        {
            get;
        }
        object Base { get; }

        IDbContextTransactionProxy BeginTransaction();

        void ChangeEntityState<TEntity>(TEntity entity, EntityState state);

        Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default);

    }
}
