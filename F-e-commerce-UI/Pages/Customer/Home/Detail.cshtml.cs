using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
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
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var findShoppingCart =
                await UnitOfWork.ShoppingCarts.GetByAsync(filter:x => x.UserId == userId && x.MenuItemId == id );
            if (findShoppingCart == null)
            {
                ShoppingCart = new()
                {
                    MenuItem = await UnitOfWork.MenuItems.GetByAsync(id: id,
                        include: $"{nameof(Category)},{nameof(FoodType)}"),
                    MenuItemId = id,
                    UserId = claim.Value
                };
            }
            else
            {
                ShoppingCart = findShoppingCart;
                ShoppingCart.MenuItem = await UnitOfWork.MenuItems.GetByAsync(id: id,
                    include: $"{nameof(Category)},{nameof(FoodType)}"); 
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var findShoppingCart =
                    await UnitOfWork.ShoppingCarts.GetByAsync(filter:x => x.UserId == ShoppingCart.UserId && x.MenuItemId == ShoppingCart.MenuItemId);
                if (findShoppingCart == null)
                {
                    await UnitOfWork.ShoppingCarts.AddAsync(ShoppingCart);
                    await UnitOfWork.SaveChangesAsync();
                }
                else
                {
                    findShoppingCart.Count = ShoppingCart.Count;
                    await UnitOfWork.ShoppingCarts.UpdateAsync(findShoppingCart);
                    await UnitOfWork.SaveChangesAsync();
                }
                
            }
            return RedirectToPage("index");
        }

    }
}
