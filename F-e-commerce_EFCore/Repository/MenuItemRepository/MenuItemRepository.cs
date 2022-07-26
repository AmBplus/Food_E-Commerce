using Domain.Dto;
using Domain.Models;
using F_e_Resources;
using Mapster;
using Microsoft.EntityFrameworkCore;
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
        if (!(Context.Entry(entity).State == EntityState.Modified))
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
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
        if (!(Context.Entry(entity).State == EntityState.Modified))
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        Context.Update(entity);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(MenuItem));
        return ViewResult.GetViewResultSucceed(message);
    }

    public MenuItemDto GetByIdDto(int id)
    {
      
        var result = Context.MenuItems.Include(x=>x.Category). 
            Include(x=>x.FoodType).
            FirstOrDefaultAsync(x => x.Id == id)
            .Adapt<MenuItemDto>();
        return result;
        
    }

    public Task<MenuItemDto> GetByIdDtoAsync(int id)
    {
        throw new NotImplementedException();
    }

    public List<MenuItemDto> GetAllMenuItemDto()
    {
        
        var result = Context.MenuItems.Include(x => x.Category).
            Include(x => x.FoodType).ProjectToType<MenuItemDto>()
            .ToList();
        return result;
    }

    public Task<List<MenuItemDto>> GetAllMenuItemDtoAsync()
    {
        throw new NotImplementedException();
    }
}
