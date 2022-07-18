using F_e_commerce_EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        // ctor
        public CreateModel(FECommerceContext commerceContext)
        {
            _commerceContext = commerceContext;
        }
        // properties
        // instance of database
        private FECommerceContext _commerceContext { get;}
        // Instance of category
        [BindProperty] public F_e_commerce_EFCore.Models.Category Category { get; set; }
        // ViewData Of Result
        [TempData]
        public string ResultStatus { get; set; }
        // methods 
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_commerceContext.Category.FirstOrDefault(x => x.Name == Category.Name) != null)
                {
                    ModelState.AddModelError("", "Duplicated Category");
                    return Page();
                }
                await _commerceContext.Category.AddAsync(Category);
                await _commerceContext.SaveChangesAsync();
                ResultStatus = $"Category {Category.Name} Created";
                return RedirectToPage("index",routeValues: ResultStatus);
            }
            return Page();
        }
    }
}
