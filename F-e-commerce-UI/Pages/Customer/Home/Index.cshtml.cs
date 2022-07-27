using System.Collections;
using Domain.Models;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        public IndexModel(IUnitOfWorkEf unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWorkEf unitOfWork { get; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public async Task OnGet()
        {
            MenuItems = await unitOfWork.MenuItems.GetAllAsync( include:$"{nameof(Category)},{nameof(FoodType)}");
            Categories = await unitOfWork.Categories.GetAllAsync(orderBy:x=>x.OrderBy(x=>x.DisplayOrder));
        }
    }
}
