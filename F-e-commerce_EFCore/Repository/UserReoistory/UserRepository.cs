using System.Linq.Expressions;
using Domain.Models;

namespace F_e_commerce_EFCore.Repository.UserReoistory;

public class UserRepository :StringRepository<ApplicationUser> , IUserRepository
{
    public UserRepository(FECommerceContext context) : base(context)
    {
      
    }

}