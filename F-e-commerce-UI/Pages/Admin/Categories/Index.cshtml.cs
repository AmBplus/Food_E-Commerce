using F_e_commerce_EFCore;
using F_e_commerce_EFCore.IUnitOfWorks;
using F_e_commerce_EFCore.Repository.CategoryRepository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        // ctor
        public IndexModel(IUnitOfWork commerceContext )
        {
            _commerceContext = commerceContext;
        }
        // properties

        // Instance Of Database
        private IUnitOfWork _commerceContext { get; set; }
        // Instance Of Categories Model
        public IEnumerable<F_e_commerce_EFCore.Models.Category> _category { get; set; }
        public void OnGet()
        {
            
            _category = _commerceContext.Categories.GetAll();
        }

    }
}
