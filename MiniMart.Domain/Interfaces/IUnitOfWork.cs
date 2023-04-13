namespace MiniMart.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangeAsync();

        Task BeginTransaction();

        Task<bool> CommitTransaction();

        Task RollbackTransaction();
    }
}
