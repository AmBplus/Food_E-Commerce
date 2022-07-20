using Domain.Models;
using F_e_commerce_EFCore.IRepository;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.CategoryRepository;

public interface ICategoryRepository : IRepository<Category>
{
    ViewResult Update(Category category);
    Task<ViewResult> UpdateAsync(Category category);
}