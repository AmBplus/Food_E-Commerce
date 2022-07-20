namespace Services.Common.Abstract.IUnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository Categories { get;}
    IFoodTypeRepository FoodTypes { get; }
    Task BeginTrans();
    Task CommitTrans();
    Task RollBack();
    bool IsDisposed { get; }
    Task SaveChangesAsync();
    void SaveChanges();
}