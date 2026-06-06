
using InventoryManagement.Application.Common.Interfaces.UnitOfWork;
using System.Data;
using System.Data.Common;

namespace InventoryManagement.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbConnection _connection;

        public UnitOfWork(DbConnection connection)
        {
            _connection = connection;
        }


        public async Task<T> ExecuteAsync<T>(Func<DbTransaction, Task<T>> operation, CancellationToken cancellationToken)
        {
            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync(cancellationToken);
            }

            await using var transaction = await _connection.BeginTransactionAsync(cancellationToken);


            try
            {
                var result = await operation(transaction);

                await transaction.CommitAsync(cancellationToken);

                return result;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

        }



        public async Task ExecuteAsync(Func<DbTransaction, Task> operation, CancellationToken cancellationToken)
        {
            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync(cancellationToken);
            }

            await using var transaction = await _connection.BeginTransactionAsync(cancellationToken);

            try
            {
                await operation(transaction);

                await transaction.CommitAsync(cancellationToken);

            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
