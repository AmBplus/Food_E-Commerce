using F_e_commerce_EFCore.IRepository;
using F_e_commerce_EFCore.Models;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.FoodRepository;

public interface IFoodTypeRepository : IRepository<FoodType>
{
    ViewResult Update(FoodType entity);
    Task<ViewResult> UpdateAsync(FoodType entity);
}