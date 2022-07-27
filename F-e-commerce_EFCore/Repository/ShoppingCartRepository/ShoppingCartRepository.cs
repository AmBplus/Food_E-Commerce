using Domain.Models;
using F_e_commerce_EFCore.Repository.FoodRepository;
using F_e_Resources;
using Mapster;
using Microsoft.EntityFrameworkCore;
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

        if (!(Context.Entry(entity).State == EntityState.Modified))
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        Context.Update(entity);
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
        if (!(Context.Entry(entity).State == EntityState.Modified))
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        Context.Update(entity);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(ShoppingCart));
        return ViewResult.GetViewResultSucceed(message);
    }

    public async Task<ViewResult> IncrementCount(int id)
    {
        var entity = await GetByAsync(id);
        if(entity == null) return new ViewResult()
            { Message = string.Format(F_e_Resources.Messages.FailMessage, nameof(IncrementCount)), Status = Convert.ToBoolean(0) };
        entity.Count++;
        await UpdateAsync(entity);
        return new ViewResult()
            { Message = string.Format(F_e_Resources.Messages.SucceedMessage, nameof(IncrementCount)), Status = Convert.ToBoolean(1) };
    }

    public async Task<ViewResult> DecrementCount(int id)
    {
        var entity = await GetByAsync(id);
        if (entity == null || entity.Count == 0) return new ViewResult()
            { Message = string.Format(F_e_Resources.Messages.FailMessage, nameof(IncrementCount)), Status = Convert.ToBoolean(0) };
        entity.Count--;
        if (entity.Count == 0)
        {
            Remove(entity);

            return new ViewResult()
                { Message = string.Format(F_e_Resources.Messages.SucceedMessage, nameof(IncrementCount)), Status = Convert.ToBoolean(1) };
        }
        await UpdateAsync(entity);
        return new ViewResult()
            { Message = string.Format(F_e_Resources.Messages.SucceedMessage, nameof(IncrementCount)), Status = Convert.ToBoolean(1) };

    }
}
