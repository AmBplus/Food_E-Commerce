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

        public IUnitOfWorkEF UnitOfWork { get; set; }
        public void OnGet()
        {

        }
    }
}
