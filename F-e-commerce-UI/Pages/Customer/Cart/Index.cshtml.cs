using System.Security.Claims;
using Domain.Models;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IndexModel(IUnitOfWorkEf unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public IUnitOfWorkEf UnitOfWork { get; set; }
        public void OnGet()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var IdUser = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCarts = UnitOfWork.ShoppingCarts.GetByFilter(
                filter: x => x.UserId == IdUser,
                include: $"{nameof(MenuItem)}," +
                         $"{nameof(MenuItem)}.{nameof(Category)}" +
                         $",{nameof(MenuItem)}.{nameof(FoodType)}");
        }

        public async Task<IActionResult> OnPostDecrementAsync(int id)
        {
           await UnitOfWork.ShoppingCarts.DecrementCount(id);
            await UnitOfWork.SaveChangesAsync();
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostIncrementAsync (int id)
        {
           await  UnitOfWork.ShoppingCarts.IncrementCount(id);
            await UnitOfWork.SaveChangesAsync();
            return RedirectToPage();
        }
        
        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            var entity = await UnitOfWork.ShoppingCarts.GetByAsync(id);
             UnitOfWork.ShoppingCarts.Remove(entity);
            await UnitOfWork.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
