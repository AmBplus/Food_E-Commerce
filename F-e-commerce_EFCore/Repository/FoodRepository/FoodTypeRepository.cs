using Domain.Dto;
using Domain.Models;
using F_e_Resources;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.FoodRepository;

public class FoodTypeRepository: Repository<FoodType> , IFoodTypeCartRepository
{
    public FoodTypeRepository(FECommerceContext context) : base(context)
    {
    }
}
