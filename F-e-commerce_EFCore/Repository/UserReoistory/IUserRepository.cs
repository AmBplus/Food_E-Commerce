using System.Linq.Expressions;
using Domain.Models;
using Services.Common.Abstract.IRepository;

namespace F_e_commerce_EFCore.Repository.UserReoistory;

public interface IUserRepository : IStringRepository<ApplicationUser> , IBaseRepository<ApplicationUser>
{
   
}