using F_e_commerce_EFCore;
using F_e_commerce_EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace F_e_commerce_UI.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        // ctor
        public DeleteModel(FECommerceContext commerceContext)
        {
            _commerceContext = commerceContext;
        }
        // properties
        // instance of database
        private FECommerceContext _commerceContext { get; set; }
        // Instance of category
        [BindProperty] public Category Category { get; set; }
        // ViewData Of Result
        [TempData]
        public string ResultStatus { get; set; }
        // methods 
        public async Task<IActionResult> OnGet(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString())) return RedirectToPage("index");
            Category = await _commerceContext.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (Category != null)
                return Page();
            return RedirectToPage("index");
        }

        public async Task<IActionResult> OnPost()
        {
            _commerceContext.Category.Remove(Category);
                await _commerceContext.SaveChangesAsync();
                ResultStatus = $"Category   Deleted";
                return RedirectToPage("index", routeValues: ResultStatus);
        }
    }
}
