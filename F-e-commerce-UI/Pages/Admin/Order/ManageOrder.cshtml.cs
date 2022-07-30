using Domain.Models;
using Domain.Vm;
using F_e_commerce_Constants;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F_e_commerce_UI.Pages.Admin.Order
{
    [Authorize(Roles =$"{Roles.MangerRole},{Roles.KitchenRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWorkEf _unitOfWork;
        public List<OrderDetailVM>? OrderDetailVM { get; set; }

        public ManageOrderModel(IUnitOfWorkEf unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task OnGetAsync()
        {
            OrderDetailVM = new();

            List<OrderHeader> orderHeaders = (await _unitOfWork.OrderHeader.GetByFilterAsync(filter:u => u.Status == StatusMessages.StatusSubmitted ||
            u.Status == StatusMessages.StatusInProcess)).ToList();

            foreach(OrderHeader item in orderHeaders)
            {
                OrderDetailVM individual = new OrderDetailVM()
                {
                    OrderHeader = item,
                  //  OrderDetails =( await _unitOfWork.OrderDetails.GetByFilterAsync(filter:u => u.OrderId == item.Id))
                };
                OrderDetailVM.Add(individual);
            }
        }

        public IActionResult OnPostOrderInProcess(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, StatusMessages.StatusInProcess);
            _unitOfWork.SaveChanges();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderReady(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, StatusMessages.StatusReady);
            _unitOfWork.SaveChanges();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, StatusMessages.StatusCancelled);
            _unitOfWork.SaveChanges();
            return RedirectToPage("ManageOrder");
        }
    }
}
