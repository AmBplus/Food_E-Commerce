using F_e_commerce_EFCore.Repository.CategoryRepository;
using F_e_commerce_EFCore.Repository.FoodRepository;

namespace F_e_commerce_EFCore.IUnitOfWorks;

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