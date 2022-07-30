using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.Order
{
    [Authorize]
    public class OrderListModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
