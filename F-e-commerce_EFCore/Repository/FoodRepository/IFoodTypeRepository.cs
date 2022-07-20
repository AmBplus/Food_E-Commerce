using Domain.Models;
using F_e_commerce_EFCore.IRepository;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.FoodRepository;

public interface IFoodTypeRepository : IRepository<FoodType>
{
    ViewResult Update(FoodType entity);
    Task<ViewResult> UpdateAsync(FoodType entity);
}