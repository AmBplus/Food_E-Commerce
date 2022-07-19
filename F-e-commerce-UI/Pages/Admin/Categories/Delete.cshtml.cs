using F_e_commerce_EFCore;
using F_e_commerce_EFCore.Models;
using F_e_commerce_EFCore.Repository.CategoryRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace F_e_commerce_UI.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        // ctor
        public DeleteModel(ICategoryRepository commerceContext)
        {
            _commerceContext = commerceContext;
        }
        // properties
        // instance of database
        private ICategoryRepository _commerceContext { get; set; }
        // Instance of category
        [BindProperty] public Category? Category { get; set; }
        // ViewData Of Result
        [TempData]
        public string ResultStatus { get; set; }
        // methods 
        public async Task<IActionResult> OnGet(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString())) return RedirectToPage("index");
            Category = await _commerceContext.GetByAsync(x => x.Id == id);
            if (Category != null)
                return Page();
            return RedirectToPage("index");
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid) return Page();
                _commerceContext.Remove(Category!);
                await _commerceContext.SaveChangesAsync();
                ResultStatus = $"Categories   Deleted";
                return RedirectToPage("index", routeValues: ResultStatus);
        }
    }
}
