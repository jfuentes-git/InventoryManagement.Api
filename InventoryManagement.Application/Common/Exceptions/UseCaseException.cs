

namespace InventoryManagement.Application.Common.Exceptions
{
    public sealed class UseCaseException : Exception
    {
        public UseCaseException(string message)
            : base(message)
        {
        }
    }
}
