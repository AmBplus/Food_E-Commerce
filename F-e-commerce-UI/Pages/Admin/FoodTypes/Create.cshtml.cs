using F_e_commerce_EFCore;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        // ctor
        public CreateModel(IUnitOfWork commerceContext)
        {
            _commerceContext = commerceContext;
        }
        // properties
        // instance of database
        private IUnitOfWork _commerceContext { get;}
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
                if ( await _commerceContext.FoodTypes.IsExitAsync(x => x.Name == FoodTypes.Name))
                {
                    ModelState.AddModelError("", "Duplicated FoodTypes");
                    return Page();
                }
                await _commerceContext.FoodTypes.AddAsync(FoodTypes);
                await _commerceContext.SaveChangesAsync();
                _commerceContext.Dispose();
                ResultStatus = $"FoodTypes {FoodTypes.Name} Created";
                return RedirectToPage("index",routeValues: ResultStatus);
            }
            return Page();
        }
    }
}
