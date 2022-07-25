using Domain.Dto;
using Domain.Models;
using Services.Common.Abstract;
using Services.Common.Abstract.IRepository;

namespace F_e_commerce_EFCore.Repository.MenuItemRepository;

public interface IMenuItemRepository : IRepository<MenuItem>
{
    ViewResult Update(MenuItem entity);
    Task<ViewResult> UpdateAsync(MenuItem entity);
    MenuItemDto GetByIdDto(int id);
    Task<MenuItemDto> GetByIdDtoAsync(int id);

    List<MenuItemDto> GetAllMenuItemDto();
    Task<List<MenuItemDto>> GetAllMenuItemDtoAsync();

}