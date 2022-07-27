using Domain.Models;
using F_e_Resources;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.CategoryRepository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(FECommerceContext context) : base(context)
    {
    }
}