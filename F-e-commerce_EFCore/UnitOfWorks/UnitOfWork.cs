using F_e_commerce_EFCore.IUnitOfWorks;
using F_e_commerce_EFCore.Repository.CategoryRepository;
using F_e_commerce_EFCore.Repository.FoodRepository;

namespace F_e_commerce_EFCore.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    FECommerceContext Context;

    public UnitOfWork(FECommerceContext context)
    {
        Context = context;
    }
    private ICategoryRepository? _CategoryRepository { get; set; }
    private IFoodTypeRepository? _FoodTypeRepository { get; set; }

    public ICategoryRepository Categories
    {
        get
        {
            return _CategoryRepository ??= new CategoryRepository(Context);
        }
    }
    public IFoodTypeRepository FoodTypes
    {
        get
        {
            return _FoodTypeRepository ??= new FoodTypeRepository(Context);
        }
    }
    public async Task SaveChangesAsync()
    {
        _ = Context.SaveChangesAsync();
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}

