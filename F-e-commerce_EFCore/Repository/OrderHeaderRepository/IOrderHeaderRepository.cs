using Domain.Dto;
using Domain.Models;
using Services.Common.Abstract;
using Services.Common.Abstract.IRepository;

namespace F_e_commerce_EFCore.Repository.OrderHeaderRepository;

public interface IOrderHeaderRepository : IRepository<OrderHeader>
{
    void UpdateStatus(int id, string status);
}