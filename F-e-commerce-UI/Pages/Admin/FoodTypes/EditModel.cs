using Domain.Models;
using F_e_commerce_EFCore;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace F_e_commerce_UI.Pages.Admin.FoodTypes
{
    public class EditModel : PageModel
    {
        // ctor
        public EditModel(IUnitOfWork commerceContext)
        {
            _commerceContext = commerceContext;
        }
        // properties
        // instance of database
        private IUnitOfWork _commerceContext { get; set; }
        // Instance of category
        [BindProperty]public FoodType? FoodType { get; set; }
        // ViewData Of Result
        [TempData]
        public string ResultStatus { get; set; }
        // methods 
        public async Task<IActionResult> OnGet(int id)
        {
            if(string.IsNullOrWhiteSpace(id.ToString())) return RedirectToPage("index");
            FoodType = await _commerceContext.FoodTypes.GetByAsync(x => x.Id == id);
            if(FoodType != null) 
            return Page();
            return RedirectToPage("index");
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _commerceContext.BeginTrans();
                _commerceContext.FoodTypes.Update(FoodType!);
                await _commerceContext.CommitTrans();
                ResultStatus = $"FoodTypes {FoodType.Name} Updated ";
                return RedirectToPage("index", routeValues: ResultStatus);
            }
            return Page();
        }
    }
}
