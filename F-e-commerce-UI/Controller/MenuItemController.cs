using F_e_commerce_EFCore.IUnitOfWorks;
using F_e_commerce_UI.wwwroot.Const;
using Microsoft.AspNetCore.Mvc;
namespace F_e_commerce_UI.Controller
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class MenuItemController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IWebHostEnvironment hostEnvironment;
        public MenuItemController(IUnitOfWorkEF unitOfWork , IWebHostEnvironment hostEnvironment) {
            UnitOfWork = unitOfWork;
            this.hostEnvironment = hostEnvironment;
        }

        public IUnitOfWorkEF UnitOfWork { get; set; }
        [HttpGet]
        public async Task<IActionResult> FetchMenuItem()
        {
            var menuItem = await UnitOfWork.MenuItems.GetAllAsync();
            return Json(new { data = menuItem });
        }
        [HttpGet]
       
        public IActionResult FetchMenuItemDto()
        {
            var menuItem =  UnitOfWork.MenuItems.GetAllMenuItemDto();
            return Json(new { data = menuItem });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var findEntity =await UnitOfWork.MenuItems.GetByAsync(id);
            if (findEntity == null) return Json(new{success=false , message ="Not Find"});
            
            if (findEntity.Image != Constance.NoImagePath )
            {
                var imagePath = hostEnvironment.WebRootPath + findEntity.Image;
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            UnitOfWork.MenuItems.Remove(findEntity);
            await UnitOfWork.SaveChangesAsync();
            return Json(new { success= true , message="Delete Successfully"  });
        }
    }
}
