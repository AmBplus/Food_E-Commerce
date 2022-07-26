using Domain.Models;
using F_e_commerce_EFCore.Repository.FoodRepository;
using F_e_Resources;
using Mapster;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.ShoppingCartRepository;

public class ShoppingCartRepository: Repository<ShoppingCart> , IShoppingCartRepository
{
    private FECommerceContext Context;
    public ShoppingCartRepository(FECommerceContext context) : base(context)
    {
        Context = context;
    }

    public ViewResult Update(ShoppingCart entity)
    {
        string message;
        if (!IsExit(x => x.Id == entity.Id))
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(ShoppingCart));
            return ViewResult.GetViewResultFailed(message);
        }

        var entityInDataBase = entity.Adapt<ShoppingCart>();
        Context.Update(entityInDataBase);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(ShoppingCart));
        return ViewResult.GetViewResultSucceed(message);
    }

    public async Task<ViewResult> UpdateAsync(ShoppingCart entity)
    {
        string message;
        if (!await IsExitAsync(x => x.Id == entity.Id))
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(ShoppingCart));
            return ViewResult.GetViewResultFailed(message);
        }
        var entityInDataBase = entity.Adapt<ShoppingCart>();
        Context.Update(entityInDataBase);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(ShoppingCart));
        return ViewResult.GetViewResultSucceed(message);
    }

}
