using F_e_commerce_EFCore.IUnitOfWorks;
using F_e_commerce_EFCore.Repository.CategoryRepository;
using F_e_commerce_EFCore.Repository.FoodRepository;

namespace F_e_commerce_EFCore.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    FECommerceContext Context;

    public UnitOfWork(FECommerceContext context) :base()
    {
        Context = context;
        //Categories = new CategoryRepository(context);
        //FoodTypes = new FoodTypeRepository(context);
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

    public async Task BeginTrans()
    {
       await Context.Database.BeginTransactionAsync();
    }

    public async Task CommitTrans()
    {
        await Context.SaveChangesAsync();
        await Context.Database.CommitTransactionAsync();

    }

    public async Task RollBack()
    {
        await Context.Database.RollbackTransactionAsync();
    }

    public bool IsDisposed { get; protected set; }

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
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async Task Dispose(bool disposing)
    {
        if(IsDisposed)return;
        if (disposing)
        {
            if (Context != null)
            {
             await   Context.DisposeAsync();
                Context = null;
                IsDisposed = true;
            }
        }
    }
}

