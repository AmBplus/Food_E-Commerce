using System.ComponentModel.DataAnnotations;
using Domain.Models;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Customer.Home
{
    public class DetailModel : PageModel
    {
        public DetailModel(IUnitOfWorkEF unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public MenuItem MenuItem { get; set; }
        private IUnitOfWorkEF UnitOfWork { get; set; }
        [Range(1, 100)] [BindProperty] public int Count { get; set; } = 0;
        public async Task OnGet(int id)
        {
            MenuItem = await UnitOfWork.MenuItems.GetByAsync(id:id,include:$"{nameof(Category)},{nameof(FoodType)}");
        }
    }
}
