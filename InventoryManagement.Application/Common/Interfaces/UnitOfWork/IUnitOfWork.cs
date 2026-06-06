using System.Data.Common;

namespace InventoryManagement.Application.Common.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<T> ExecuteAsync<T>(Func<DbTransaction, Task<T>> operation,CancellationToken cancellationToken);
        Task ExecuteAsync(Func<DbTransaction, Task> operation, CancellationToken cancellationToken);
    }

}
