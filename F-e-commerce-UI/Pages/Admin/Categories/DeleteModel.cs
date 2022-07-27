using Domain.Models;
using F_e_commerce_EFCore;
using F_e_commerce_EFCore.IUnitOfWorks;
using F_e_commerce_EFCore.Repository.CategoryRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace F_e_commerce_UI.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        // ctor
        public DeleteModel(IUnitOfWorkEf commerceContext)
        {
            _commerceContext = commerceContext;
        }
        // properties
        // instance of database
        private IUnitOfWorkEf _commerceContext { get; set; }
        // Instance of category
        [BindProperty] public Category Category { get; set; }
        // ViewData Of Result
        [TempData]
        public string ResultStatus { get; set; }
        // methods 
        public async Task<IActionResult> OnGet(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString())) return RedirectToPage("index");
            Category = await _commerceContext.Categories.GetByAsync(x => x.Id == id);
            if (Category != null)
                return Page();
            return RedirectToPage("index");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();
            //await _commerceContext.BeginTrans();
            _commerceContext.Categories.Remove(Category!);
            //await _commerceContext.CommitTrans();
            await _commerceContext.SaveChangesAsync();
            ResultStatus = $"Categories   Deleted";
            return RedirectToPage("index", routeValues: ResultStatus);
        }
    }
}
