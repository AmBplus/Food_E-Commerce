using F_e_commerce_EFCore.Models;
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
        Context.Update(category);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(category));
        return ViewResult.GetViewResultSucceed(message);


    }
    public async Task<ViewResult> UpdateAsync(Category category)
    {
        string message;
        var findCategory =  GetByAsync(category.Id);
        if (category == null)
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(category));
            return  ViewResult.GetViewResultFailed(message);
        }
        Context.Update(category);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(category));
        return ViewResult.GetViewResultSucceed(message);
    }

}