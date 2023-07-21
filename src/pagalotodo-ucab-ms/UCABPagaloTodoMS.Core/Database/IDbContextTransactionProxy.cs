using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Core.Database
{
    
    public interface IDbContextTransactionProxy : IDisposable
    {
        void Commit();
        void Rollback();
    }
}


