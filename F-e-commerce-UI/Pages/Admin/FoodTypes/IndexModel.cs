using Domain.Models;
using F_e_commerce_EFCore;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        // ctor
        public IndexModel(IUnitOfWorkEf commerceContext )
        {
            _commerceContext = commerceContext;
        }
        // properties

        // Instance Of Database
        private IUnitOfWorkEf _commerceContext { get; set; }
        // Instance Of FoodTypes Model
        public IEnumerable<FoodType> _FoodTypes { get; set; }
        public void OnGet()
        {
            _FoodTypes = _commerceContext.FoodTypes.GetAll();
        }

    }
}
