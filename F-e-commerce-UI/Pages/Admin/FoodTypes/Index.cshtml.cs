using F_e_commerce_EFCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        // ctor
        public IndexModel(FECommerceContext commerceContext )
        {
            _commerceContext = commerceContext;
        }
        // properties

        // Instance Of Database
        private FECommerceContext _commerceContext { get; set; }
        // Instance Of FoodTypes Model
        public IEnumerable<F_e_commerce_EFCore.Models.FoodType> _FoodTypes { get; set; }
        public void OnGet()
        {
            
            _FoodTypes = _commerceContext.FoodTypes;
        }

    }
}
