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
    public List<MenuItemDto> GetAllMenuItemDto()
    {
        var result = Context.MenuItems.Include
                (x => x.Category).
            Include(x => x.FoodType).ProjectToType<MenuItemDto>()
            .ToList();
        return result;
    }
}
