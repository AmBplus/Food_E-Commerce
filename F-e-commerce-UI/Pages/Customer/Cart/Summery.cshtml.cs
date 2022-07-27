using System.Security.Claims;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Customer.Cart
{
    public class SummeryModel : PageModel
    {
        public SummeryModel(IUnitOfWorkEf unitOfWorkEf)
        {
            UnitOfWorkEf = unitOfWorkEf;
        }

        public IUnitOfWorkEf UnitOfWorkEf { get; set; }
        public void OnGet()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var findUser = UnitOfWorkEf.Users.GetBy(userId);
        }
    }
}
