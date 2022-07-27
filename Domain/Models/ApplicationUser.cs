using Microsoft.AspNetCore.Identity;
using Services.Common.Abstract;

namespace Domain.Models;

public class ApplicationUser : IdentityUser , IBaseModel<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; } 
    public string PhoneNumber { get; set; }
}