using Domain.Models;
using F_e_Resources;
using Mapster;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.FoodRepository;

public class FoodTypeRepository: Repository<FoodType> , IFoodTypeRepository
{
    private FECommerceContext Context;
    public FoodTypeRepository(FECommerceContext context) : base(context)
    {
        Context = context;
    }

    public ViewResult Update(FoodType entity)
    {
        string message;
        if (!IsExit(x => x.Id == entity.Id))
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(FoodType));
            return ViewResult.GetViewResultFailed(message);
        }

        var entityInDataBase = entity.Adapt<FoodType>();
        Context.Update(entityInDataBase);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(FoodType));
        return ViewResult.GetViewResultSucceed(message);
    }

    public async Task<ViewResult> UpdateAsync(FoodType entity)
    {
        string message;
        if (!await IsExitAsync(x => x.Id == entity.Id))
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(FoodType));
            return ViewResult.GetViewResultFailed(message);
        }
        var entityInDataBase = entity.Adapt<FoodType>();
        Context.Update(entityInDataBase);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(FoodType));
        return ViewResult.GetViewResultSucceed(message);
    }
}
