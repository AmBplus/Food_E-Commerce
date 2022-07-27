using Domain.Models;
using F_e_Resources;
using Microsoft.EntityFrameworkCore;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.OrderDetailRepository;

public class OrderDetailRepository :Repository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(FECommerceContext context) : base(context)
    {
    }

   
}