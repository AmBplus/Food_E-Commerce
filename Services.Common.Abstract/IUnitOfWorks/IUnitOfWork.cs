namespace Services.Common.Abstract.IUnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    Task BeginTrans();
    Task CommitTrans();
    Task RollBack();
    bool IsDisposed { get; }
    Task SaveChangesAsync();
    void SaveChanges();
}