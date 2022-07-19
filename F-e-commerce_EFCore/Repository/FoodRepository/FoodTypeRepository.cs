using F_e_commerce_EFCore.Models;
using F_e_Resources;
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
        var entityInDataBase = GetBy(entity.Id);
        if (entityInDataBase == null)
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(FoodType));
            return ViewResult.GetViewResultFailed(message);
        }
        Context.Update(entity);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(FoodType));
        return ViewResult.GetViewResultSucceed(message);
    }

    public async Task<ViewResult> UpdateAsync(FoodType entity)
    {
        string message;
        var entityInDataBase = GetByAsync(entity.Id);
        if (entityInDataBase == null)
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(FoodType));
            return ViewResult.GetViewResultFailed(message);
        }
        Context.Update(entity);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(FoodType));
        return ViewResult.GetViewResultSucceed(message);
    }
}
