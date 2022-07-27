using F_e_commerce_EFCore.IUnitOfWorks;
using F_e_commerce_EFCore.Repository.CategoryRepository;
using F_e_commerce_EFCore.Repository.FoodRepository;
using F_e_commerce_EFCore.Repository.MenuItemRepository;
using F_e_commerce_EFCore.Repository.OrderDetailRepository;
using F_e_commerce_EFCore.Repository.OrderHeaderRepository;
using F_e_commerce_EFCore.Repository.ShoppingCartRepository;
using F_e_commerce_EFCore.Repository.UserReoistory;

namespace F_e_commerce_EFCore.UnitOfWorks;

public class UnitOfWorkEf : UnitOfWork, IUnitOfWorkEf
{
    FECommerceContext Context;
    public UnitOfWorkEf(FECommerceContext context) : base(context)
    {
        Context = context;
    }
    // Private Property 
    // ************************************************************************

    #region Private Property

    private ICategoryRepository? _CategoryRepository { get; set; }
    private IFoodTypeCartRepository? _FoodTypeRepository { get; set; }
    private IMenuItemRepository? _MenuItemRepository { get; set; }
    private IShoppingCartRepository? _ShoppingCartRepository { get; set; }
    private IOrderHeaderRepository? _OrderHeaderRepository { get; set; }
    private IOrderDetailRepository? _OrderDetailRepository { get; set; }
    private IUserRepository? _UserRepository { get; set; }

    #endregion Private Property

    // ************************************************************************

    // Public Property 
    // ************************************************************************

    #region Public Property 

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
    public IMenuItemRepository MenuItems
    {
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

    public IOrderHeaderRepository OrderHeader
    {
        get
        {
            return _OrderHeaderRepository ??= new OrderHeaderRepository(Context);
        }
    }

    public IOrderDetailRepository OrderDetails
    {
        get
        {
            return _OrderDetailRepository ??= new OrderDetailRepository(Context);
        }
    }

    public IUserRepository Users
    {
        get
        {
            return _UserRepository ??= new UserRepository(context: Context);
        }
    }

    #endregion Public Property 
    // ************************************************************************
}

