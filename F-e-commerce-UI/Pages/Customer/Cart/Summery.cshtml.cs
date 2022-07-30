using System.Security.Claims;
using CommonUtility;
using Domain.Models;
using F_e_commerce_Constants;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace F_e_commerce_UI.Pages.Customer.Cart
{
    [Authorize]
    public class SummeryModel : PageModel
    {
        public SummeryModel(IUnitOfWorkEf unitOfWorkEf, IOptions<StripeSettings> Strips)
        {
            UnitOfWorkEf = unitOfWorkEf;
        }
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        [BindProperty]
        public OrderHeader OrderHeader { get; set; }
        private IUnitOfWorkEf UnitOfWorkEf { get; set; }
        public async Task<IActionResult> OnGet()
        {
            OrderHeader = new OrderHeader();
            var claimesIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimesIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var userId = claim.Value;
                ShoppingCarts = await UnitOfWorkEf.ShoppingCarts.GetByFilterAsync(filter: x => x.UserId == userId
                    , include: $"{nameof(MenuItem)},{nameof(MenuItem)}.{nameof(FoodType)},{nameof(MenuItem)}.{nameof(Category)}");
                if (ShoppingCarts == null)
                {
                    return RedirectToPage(nameof(EmptyOrderModel));
                }

                foreach (var cartItem in ShoppingCarts)
                {
                    OrderHeader.OrderTotal += cartItem.MenuItem.Price * cartItem.Count;
                }

                ApplicationUser applicationUser = await UnitOfWorkEf.Users.GetByAsync
                    (x => x.Id == userId);
                OrderHeader.Name = applicationUser.FirstName + " " + applicationUser.LastName;
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                #region RegisterByOrderClaim

                var userId = claims.Value;
                ShoppingCarts = await UnitOfWorkEf.ShoppingCarts.GetByFilterAsync(filter: x => x.UserId == userId
                    , include: $"{nameof(MenuItem)},{nameof(MenuItem)}.{nameof(FoodType)},{nameof(MenuItem)}.{nameof(Category)}");
                foreach (var cartItem in ShoppingCarts)
                {
                    OrderHeader.OrderTotal += cartItem.MenuItem.Price * cartItem.Count;
                }

                OrderHeader.Status = StatusMessages.StatusPending;
                OrderHeader.UserId = userId;
                OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.OrderDate.ToShortDateString() + " " + OrderHeader.PickUpTime.ToLongTimeString());
                OrderHeader.OrderDate = DateTime.Now;
                UnitOfWorkEf.OrderHeader.Add(OrderHeader);
                UnitOfWorkEf.SaveChanges();

                #endregion
                foreach (var shopItem in ShoppingCarts)
                {
                    var orderDetail = new OrderDetail()
                    {
                        Count = shopItem.Count,
                        MenuItemId = shopItem.MenuItemId,
                        OrderId = OrderHeader.Id,
                        Price = shopItem.MenuItem.Price,
                        Name = shopItem.MenuItem.Name
                    };
                    UnitOfWorkEf.OrderDetails.Add(orderDetail);
                    UnitOfWorkEf.SaveChanges();
                }
                #region Payment
                var domain = "https://localhost:7018/Customer/Cart";
                var quantity = ShoppingCarts.ToList().Count;
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>(),
                    PaymentMethodTypes = new List<string>(){"card"}
                    ,
                    Mode = "payment",
                    SuccessUrl = domain + $"/OrderConfirmation?id={OrderHeader.Id}",
                    CancelUrl = domain + "/CancelOrder",
                };
                foreach (var item in ShoppingCarts)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                        PriceData = new SessionLineItemPriceDataOptions()
                        {
                            UnitAmount = (long)item.MenuItem.Price * 100,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions()
                            {
                                Name = item.MenuItem.Name
                            },
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                UnitOfWorkEf.ShoppingCarts.RemoveRange(ShoppingCarts);
                UnitOfWorkEf.SaveChanges();

                var service = new SessionService();
                Session session = service.Create(options);
                Response.Headers.Add("Location", session.Url);

                //OrderHeader.SessionId = session.Id;
                //OrderHeader.PaymentIntentId = session.PaymentIntentId;
                UnitOfWorkEf.SaveChangesAsync();
                return new StatusCodeResult(303);

                #endregion

            }
            return Page();

        }
    }
}
