using Domain.Models;
using F_e_Resources;
using Microsoft.EntityFrameworkCore;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.Repository.OrderHeaderRepository;

public class OrderHeaderRepository : Repository<OrderHeader> , IOrderHeaderRepository
{
    public OrderHeaderRepository(FECommerceContext context) : base(context)
    {
        Context = context;
    }

    FECommerceContext Context;

    public void UpdateStatus(int id, string status)
    {
        var orderFromDb = Context.OrderHeaders.FirstOrDefault(u => u.Id == id);
        if (orderFromDb != null)
        {
            orderFromDb.Status = status;
        }
    }

}