using Domain.Models;
using F_e_commerce_EFCore.Repository.FoodRepository;
using F_e_Resources;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.ShoppingCartRepository;

public class ShoppingCartRepository: Repository<ShoppingCart> , IShoppingCartRepository
{
    private FECommerceContext Context;
    public ShoppingCartRepository(FECommerceContext context) : base(context)
    {
        Context = context;
    }
    /// <summary>
    /// IncrementCount By One 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Update Shopping Cart Count By Plus One And With With Result Message</returns>
    public async Task<ViewResult> IncrementCount(int id)
    {
        var entity = await GetByAsync(id);
        if(entity == null) return new ViewResult()
            { Message = string.Format(F_e_Resources.Messages.FailMessage, nameof(IncrementCount)), Status = Convert.ToBoolean(0) };
        entity.Count++;
        await UpdateAsync(entity);
        return new ViewResult()
            { Message = string.Format(F_e_Resources.Messages.SucceedMessage, nameof(IncrementCount)), Status = Convert.ToBoolean(1) };
    }
    /// <summary>
    /// DecrementCount By One 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Update Shopping Cart Count By Minus One And With With Result Message</returns>
    public async Task<ViewResult> DecrementCount(int id)
    {
        var entity = await GetByAsync(id);
        if (entity == null || entity.Count == 0) return new ViewResult()
            { Message = string.Format(F_e_Resources.Messages.FailMessage, nameof(IncrementCount)), Status = Convert.ToBoolean(0) };
        entity.Count--;
        if (entity.Count == 0)
        {
            Remove(entity);

            return new ViewResult()
                { Message = string.Format(F_e_Resources.Messages.SucceedMessage, nameof(IncrementCount)), Status = Convert.ToBoolean(1) };
        }
        await UpdateAsync(entity);
        return new ViewResult()
            { Message = string.Format(F_e_Resources.Messages.SucceedMessage, nameof(IncrementCount)), Status = Convert.ToBoolean(1) };

    }
}
