using Domain.Models;

namespace Domain.Vm
{
    public class OrderDetailVM
    {
        public OrderHeader OrderHeader { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
