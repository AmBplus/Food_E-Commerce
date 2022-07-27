using Domain.Dto;
using Domain.Models;
using Services.Common.Abstract;
using Services.Common.Abstract.IRepository;

namespace F_e_commerce_EFCore.Repository.MenuItemRepository;

public interface IMenuItemRepository : IRepository<MenuItem>
{
    List<MenuItemDto> GetAllMenuItemDto();
}