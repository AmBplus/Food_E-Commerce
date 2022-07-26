using Domain.Models;
using Services.Common.Abstract;
using Services.Common.Abstract.IRepository;

namespace F_e_commerce_EFCore.Repository.ShoppingCartRepository;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    ViewResult Update(ShoppingCart entity);
    Task<ViewResult> UpdateAsync(ShoppingCart entity); 
}