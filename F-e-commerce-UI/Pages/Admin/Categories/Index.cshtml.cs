using F_e_commerce_EFCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.Categories
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
        // Instance Of Category Model
        public IEnumerable<F_e_commerce_EFCore.Models.Category> _category { get; set; }
        public void OnGet()
        {
            
            _category = _commerceContext.Category;
        }

    }
}
