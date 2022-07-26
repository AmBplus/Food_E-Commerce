using Domain.Dto;
using Domain.Models;
using Services.Common.Abstract;
using Services.Common.Abstract.IRepository;

namespace F_e_commerce_EFCore.Repository.FoodRepository;

public interface IFoodTypeCartRepository : IRepository<FoodType>
{
    ViewResult Update(FoodType entity);
    Task<ViewResult> UpdateAsync(FoodType entity); 
}