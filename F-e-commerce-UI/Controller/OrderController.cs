using Domain.Models;
using F_e_commerce_Constants;
using F_e_commerce_EFCore.IUnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace F_e_commerce_UI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUnitOfWorkEf _unitOfWork;

        public OrderController(IUnitOfWorkEf unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
      //  [Authorize]
        public async Task<IActionResult> GetAllOrderHeader()
        {

            var OrderHeaderList = _unitOfWork.OrderHeader.GetAll();
            var list = OrderHeaderList.ToList();

            //if(status== "cancelled")
            //{
            //    OrderHeaderList = OrderHeaderList.Where(u => u.Status == StatusMessages.StatusCancelled || u.Status == StatusMessages.StatusRejected);
            //}
            //else
            //{
            //    if (status == "completed")
            //    {
            //        OrderHeaderList = OrderHeaderList.Where(u => u.Status == StatusMessages.StatusCompleted );
            //    }
            //    else
            //    {
            //        if (status == "ready")
            //        {
            //            OrderHeaderList = OrderHeaderList.Where(u => u.Status == StatusMessages.StatusReady);
            //        }
            //        else
            //        {
            //            OrderHeaderList = OrderHeaderList.Where(u => u.Status == StatusMessages.StatusSubmitted || u.Status == StatusMessages.StatusInProcess);
            //        }
            //    }
            //}

            return Json(new { data = OrderHeaderList });
        }

        
    }
}
