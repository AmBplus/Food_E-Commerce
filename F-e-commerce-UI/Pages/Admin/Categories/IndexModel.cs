using Domain.Models;
using F_e_commerce_EFCore;
using F_e_commerce_EFCore.IUnitOfWorks;
using F_e_commerce_EFCore.Repository.CategoryRepository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.Categories
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
        // Instance Of Categories Model
        public IEnumerable<Category> _category { get; set; }
        public void OnGet()
        {
            
            _category = _commerceContext.Categories.GetAll();
        }

    }
}
