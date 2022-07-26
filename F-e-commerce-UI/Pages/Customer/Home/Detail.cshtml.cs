using System.ComponentModel.DataAnnotations;
using Domain.Models;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Customer.Home
{
    [Authorize]
    public class DetailModel : PageModel
    {
        public DetailModel(IUnitOfWorkEF unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        [BindProperty] public ShoppingCart ShoppingCart { get; set; }
        private IUnitOfWorkEF UnitOfWork { get; set; }
        public async Task OnGet(int id)
        {
            ShoppingCart = new()
            {
                MenuItem = await UnitOfWork.MenuItems.GetByAsync(id: id,
                    include: $"{nameof(Category)},{nameof(FoodType)}")
            };

        }

        public async Task<IActionResult> OnPostAsync()
        {

            //UnitOfWork.ShoppingCarts.Add(ShoppingCart);
            return RedirectToPage("index");
        }

    }
}
