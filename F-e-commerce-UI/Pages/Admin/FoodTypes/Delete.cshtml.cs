using F_e_commerce_EFCore;
using F_e_commerce_EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace F_e_commerce_UI.Pages.Admin.FoodTypes
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
        [BindProperty] public FoodType FoodType { get; set; }
        // ViewData Of Result
        [TempData]
        public string ResultStatus { get; set; }
        // methods 
        public async Task<IActionResult> OnGet(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString())) return RedirectToPage("index");
            FoodType = await _commerceContext.FoodTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (FoodType != null)
                return Page();
            return RedirectToPage("index");
        }

        public async Task<IActionResult> OnPost()
        {
            _commerceContext.FoodTypes.Remove(FoodType);
                await _commerceContext.SaveChangesAsync();
                ResultStatus = $"FoodTypes   Deleted";
                return RedirectToPage("index", routeValues: ResultStatus);
        }
    }
}
