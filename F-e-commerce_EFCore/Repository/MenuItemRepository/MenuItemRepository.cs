using Domain.Models;
using F_e_Resources;
using Mapster;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.MenuItemRepository;

public class MenuItemRepository : Repository<MenuItem> , IMenuItemRepository
{
    private FECommerceContext Context;
    public MenuItemRepository(FECommerceContext context) : base(context)
    {
        Context = context;
    }

    public ViewResult Update(MenuItem entity)
    {
        string message;
        if (!IsExit(x => x.Id == entity.Id))
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(MenuItem));
            return ViewResult.GetViewResultFailed(message);
        }
        var entityInDataBase = entity.Adapt<MenuItem>();
        Context.Update(entity);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(MenuItem));
        return ViewResult.GetViewResultSucceed(message);
    }

    public async Task<ViewResult> UpdateAsync(MenuItem entity)
    {
        string message;
        if (! await IsExitAsync(x=>x.Id== entity.Id))
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(MenuItem));
            return ViewResult.GetViewResultFailed(message);
        }
        var entityInDataBase= entity.Adapt<MenuItem>();
        Context.Update(entityInDataBase);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(MenuItem));
        return ViewResult.GetViewResultSucceed(message);
    }
}
