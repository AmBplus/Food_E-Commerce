using Domain.Models;
using F_e_Resources;
using Microsoft.EntityFrameworkCore;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.OrderHeaderRepository;

public class OrderHeaderRepository : Repository<OrderHeader> , IOrderHeaderRepository
{
    public OrderHeaderRepository(FECommerceContext context) : base(context)
    {
    }
}