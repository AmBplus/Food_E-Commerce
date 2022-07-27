using F_e_commerce_EFCore.IUnitOfWorks;
using F_e_commerce_EFCore.Repository.CategoryRepository;
using F_e_commerce_EFCore.Repository.FoodRepository;
using F_e_commerce_EFCore.Repository.MenuItemRepository;
using F_e_commerce_EFCore.Repository.ShoppingCartRepository;

namespace F_e_commerce_EFCore.UnitOfWorks;

public class UnitOfWorkEF : IUnitOfWorkEF
{
    FECommerceContext Context;

    public UnitOfWorkEF(FECommerceContext context) :base()
    {
        Context = context;
    }
    private ICategoryRepository? _CategoryRepository { get; set; }
    private IFoodTypeCartRepository? _FoodTypeRepository { get; set; }
    private IMenuItemRepository? _MenuItemRepository { get; set; }
    private IShoppingCartRepository? _ShoppingCartRepository { get; set; }
    public ICategoryRepository Categories
    {
        get
        {
            return _CategoryRepository ??= new CategoryRepository(Context);
        }
    }

    public IFoodTypeCartRepository FoodTypes
    {
        get
        {
            return _FoodTypeRepository ??= new FoodTypeRepository(Context);
        }
    }

    public IMenuItemRepository MenuItems {
        get
        {
            return _MenuItemRepository ??= new MenuItemRepository(Context);
        }
    }

    public IShoppingCartRepository ShoppingCarts
    {
        get
        {
            return _ShoppingCartRepository ??= new ShoppingCartRepository(Context);
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
             Context.Dispose();
                Context = null;
                IsDisposed = true;
            }
        }
    }
}

