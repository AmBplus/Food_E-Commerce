using Domain.Models;
using Domain.Vm;
using F_e_commerce_Constants;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace F_e_commerce_UI.Pages.Admin.Order
{
    public class OrderDetailsModel : PageModel
    {
        private readonly IUnitOfWorkEf _unitOfWork;
        
        public OrderDetailVM OrderDetailVM { get; set; }
        public OrderDetailsModel(IUnitOfWorkEf unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task OnGetAsync(int id)
        {
            OrderDetailVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetBy(u => u.Id == id, include: nameof(ApplicationUser)),
                OrderDetails = (await _unitOfWork.OrderDetails.GetByFilterAsync(filter:u => u.OrderId == id)).ToList()
            };
        }

        public IActionResult OnPostOrderCompleted(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, StatusMessages.StatusCompleted);
            _unitOfWork.SaveChanges();
            return RedirectToPage("OrderList");
        }
        public IActionResult OnPostOrderRefund(int orderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetBy(o => o.Id == orderId);

            var options = new RefundCreateOptions
            {
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = orderHeader.PaymentIntentId
            };

            var service = new RefundService();
            Refund refund = service.Create(options);

            _unitOfWork.OrderHeader.UpdateStatus(orderId, StatusMessages.StatusRefunded);
            _unitOfWork.SaveChanges();
            return RedirectToPage("OrderList");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, StatusMessages.StatusCancelled);
            _unitOfWork.SaveChanges();
            return RedirectToPage("OrderList");
        }
    }
}
