﻿using F_e_commerce_EFCore.Repository.CategoryRepository;
using F_e_commerce_EFCore.Repository.FoodRepository;
using Services.Common.Abstract.IUnitOfWorks;

namespace F_e_commerce_EFCore.IUnitOfWorks;

public interface IUnitOfWorkEF :IUnitOfWork
{
    ICategoryRepository Categories { get;}
    IFoodTypeRepository FoodTypes { get; }

}