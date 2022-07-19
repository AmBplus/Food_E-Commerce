using F_e_commerce_EFCore;
using F_e_commerce_EFCore.Repository.CategoryRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        // ctor
        public CreateModel(ICategoryRepository commerceContext)
        {
            _commerceContext = commerceContext;
        }
        // properties
        // instance of database
        private ICategoryRepository _commerceContext { get;}
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
                if (await _commerceContext.IsExitAsync(x => x.Name == Category.Name))
                {
                    ModelState.AddModelError("", "Duplicated Categories");
                    return Page();
                }
                await _commerceContext.AddAsync(Category);
                await _commerceContext.SaveChangesAsync();
                ResultStatus = $"Categories {Category.Name} Created";
                return RedirectToPage("index",routeValues: ResultStatus);
            }
            return Page();
        }
    }
}
