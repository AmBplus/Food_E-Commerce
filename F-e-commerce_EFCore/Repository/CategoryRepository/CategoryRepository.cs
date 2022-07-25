using Domain.Models;
using F_e_Resources;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.CategoryRepository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private FECommerceContext Context;
    public CategoryRepository(FECommerceContext context) : base(context)
    {
        Context = context;
    }
    public ViewResult Update(Category category)
    {
        string message;
        var Category = GetBy(category.Id);
        if (category == null)
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(category));
            return ViewResult.GetViewResultFailed(message);
        }
        Category.Name = category.Name;
        Category.DisplayOrder = category.DisplayOrder;
        Context.Update(Category);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(category));
        return ViewResult.GetViewResultSucceed(message);
    }
    public async Task<ViewResult> UpdateAsync(Category category)
    {
        string message;
        var findCategory = await GetByAsync(category.Id);
        if (category == null)
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(category));
            return  ViewResult.GetViewResultFailed(message);
        }
        findCategory.Name = category.Name;
        findCategory.DisplayOrder = category.DisplayOrder;
        Context.Update(findCategory);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(category));
        return ViewResult.GetViewResultSucceed(message);
    }

}