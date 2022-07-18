using F_e_commerce_EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.FoodTypes
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
        [BindProperty] public F_e_commerce_EFCore.Models.FoodType FoodTypes { get; set; }
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
                if (_commerceContext.Categories.FirstOrDefault(x => x.Name == FoodTypes.Name) != null)
                {
                    ModelState.AddModelError("", "Duplicated FoodTypes");
                    return Page();
                }
                await _commerceContext.FoodTypes.AddAsync(FoodTypes);
                await _commerceContext.SaveChangesAsync();
                ResultStatus = $"FoodTypes {FoodTypes.Name} Created";
                return RedirectToPage("index",routeValues: ResultStatus);
            }
            return Page();
        }
    }
}
