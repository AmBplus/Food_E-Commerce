using Domain.Models;
using F_e_commerce_Constants;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;

namespace F_e_commerce_UI.Pages.Customer.Cart
{
    public class OrderConfirmationModel : PageModel
    {
        private IUnitOfWorkEf _unitOfWork;
        public int OrderId { get; set; }

        public OrderConfirmationModel(IUnitOfWorkEf unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetBy(u => u.Id == id);
            if (orderHeader.SessionId != null)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    orderHeader.Status = StatusMessages.StatusSubmitted;
                    _unitOfWork.SaveChanges();
                }
            }
            List<ShoppingCart> shoppingCarts =
                _unitOfWork.ShoppingCarts.GetByFilter(filter: u => u.UserId == orderHeader.UserId).ToList();
            _unitOfWork.ShoppingCarts.RemoveRange(shoppingCarts);
            _unitOfWork.SaveChanges();
            OrderId = id;
        }
    }
}
