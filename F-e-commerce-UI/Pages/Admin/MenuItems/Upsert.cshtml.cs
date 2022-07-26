using System.Collections;
using Domain.Dto;
using Domain.Models;
using F_e_commerce_EFCore.IUnitOfWorks;
using F_e_commerce_UI.wwwroot.Const;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace F_e_commerce_UI.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        public UpsertModel(IUnitOfWorkEF unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            UnitOfWork = unitOfWork;
            HostEnvironment = hostEnvironment;
        }

        private IWebHostEnvironment HostEnvironment { get; set; }
        public MenuItem? MenuItem { get; set; }

        public IFormFile? file { get; set; }

        public IEnumerable<SelectListItem> CategoryListItems { get; set; }

        public IEnumerable<SelectListItem> FoodTypeListItems { get; set; }


        private IUnitOfWorkEF UnitOfWork { get; set; }
        public async Task OnGet(int? id)
        {
            MenuItem = new MenuItem();
            if (id != null)
#pragma warning disable CS8629 // Nullable value type may be null.
                MenuItem = await UnitOfWork.MenuItems.GetByAsync((int)id, "Category,FoodType");
#pragma warning restore CS8629 // Nullable value type may be null.
            CategoryListItems = await GetCategoryListItems();
            FoodTypeListItems = await GetFoodTypesListItems();
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoryListItems()
        {
            return (await UnitOfWork.Categories.GetAllAsync()).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
        }
        public async Task<IEnumerable<SelectListItem>> GetFoodTypesListItems()
        {
            return (await UnitOfWork.FoodTypes.GetAllAsync()).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
        }
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1000000)]
        public async Task<IActionResult> OnPost()
        {
            int errorCountBeforeSent = ModelState.ErrorCount;
            CategoryListItems = await GetCategoryListItems();
            FoodTypeListItems = await GetFoodTypesListItems();
            CheckModelValidation(ModelState, MenuItem!);
            if (errorCountBeforeSent < ModelState.ErrorCount)
            {
                return Page();
            }

            if (MenuItem.Id == 0)
            {
                if (file is { Length: > 0 })
                {
                    var secureFileName = Path.GetFileName(file.FileName);
                    var extenstion = Path.GetExtension(secureFileName);
                    var rootPath = HostEnvironment.WebRootPath;
                    var newFileName = Guid.NewGuid() + extenstion;
                    var pathFolderMenuItem = @"\Images\MenuItem";
                    var menuImagesPath = rootPath + pathFolderMenuItem;
                    using (var fileStream = new FileStream(Path.Combine(menuImagesPath, newFileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    MenuItem!.Image = pathFolderMenuItem + "\\" + newFileName;
                    await UnitOfWork.BeginTrans();
                    await UnitOfWork.MenuItems.AddAsync(MenuItem);
                    await UnitOfWork.CommitTrans();
                }
                else
                {
                    MenuItem.Image = Constance.NoImagePath;
                    await UnitOfWork.BeginTrans();
                    await UnitOfWork.MenuItems.AddAsync(MenuItem);
                    await UnitOfWork.CommitTrans();
                }
            }
            else
            {
                var findMenuItem =await UnitOfWork.MenuItems.GetByAsync(x => x.Id == MenuItem.Id);

                if (file is { Length: > 0 })
                {
                    var secureFileName = Path.GetFileName(file.FileName);
                    var extenstion = Path.GetExtension(secureFileName);
                    var rootPath = HostEnvironment.WebRootPath;
                    var newFileName = Guid.NewGuid() + extenstion;
                    var pathFolderMenuItem = @"\Images\MenuItem";
                    var menuImagesPath = rootPath + pathFolderMenuItem;
                    using (var fileStream = new FileStream(Path.Combine(menuImagesPath, newFileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    if (findMenuItem.Image != Constance.NoImagePath)
                    {
                        var imagePath = HostEnvironment.WebRootPath + findMenuItem.Image;
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    findMenuItem!.Image = pathFolderMenuItem + "\\" + newFileName;
                    UnitOfWork.MenuItems.Update(findMenuItem);
                    await UnitOfWork.SaveChangesAsync();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(findMenuItem.Image)) findMenuItem.Image = Constance.NoImagePath;
                    UnitOfWork.MenuItems.Update(findMenuItem);
                    await UnitOfWork.SaveChangesAsync();
                }
            }
            return RedirectToPage("/Admin/MenuItems/index");
        }

        public void CheckModelValidation(ModelStateDictionary modelState, MenuItem model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                modelState.AddModelError("", $"Menuitem Name IsNullOrWhiteSpace");

            if (string.IsNullOrWhiteSpace(model.Description))
                modelState.AddModelError("", $"Menuitem Description IsNullOrWhiteSpace");

            if (string.IsNullOrWhiteSpace(model.Price.ToString()))
                modelState.AddModelError("", $"Menuitem Price IsNullOrWhiteSpace");

            if (string.IsNullOrWhiteSpace(model.ForeignKeyCategory.ToString()))
                modelState.AddModelError("", $"Menuitem ForeignKeyCategory IsNullOrWhiteSpace");

            if (string.IsNullOrWhiteSpace(model.ForeignKeyFoodType.ToString()))
                modelState.AddModelError("", $"Menuitem ForeignKeyFoodType IsNullOrWhiteSpace");

            if (model.Price < 0) modelState.AddModelError("", $"Menuitem Price UnValid");
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var exstention = Path.GetExtension(fileName);
                if (exstention == ".png" || exstention == ".jpg" || exstention == ".jpge" || exstention == ".webp") return;
                modelState.AddModelError("", "UnValid Image File");
            }
        }
    }
}
