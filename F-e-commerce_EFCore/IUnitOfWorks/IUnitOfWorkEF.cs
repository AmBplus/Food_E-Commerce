using F_e_commerce_EFCore.Repository.CategoryRepository;
using F_e_commerce_EFCore.Repository.FoodRepository;
using F_e_commerce_EFCore.Repository.MenuItemRepository;
using F_e_commerce_EFCore.Repository.OrderDetailRepository;
using F_e_commerce_EFCore.Repository.OrderHeaderRepository;
using F_e_commerce_EFCore.Repository.ShoppingCartRepository;
using F_e_commerce_EFCore.Repository.UserReoistory;
using Services.Common.Abstract.IUnitOfWorks;

namespace F_e_commerce_EFCore.IUnitOfWorks;

public interface IUnitOfWorkEf :IUnitOfWork
{
    ICategoryRepository Categories { get;}
    IFoodTypeCartRepository FoodTypes { get; }
    IMenuItemRepository MenuItems { get; }
    IShoppingCartRepository ShoppingCarts { get; }
    IOrderHeaderRepository OrderHeader { get; }
    IOrderDetailRepository OrderDetails { get; }
    IUserRepository Users { get; }

}