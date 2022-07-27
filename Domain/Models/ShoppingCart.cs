using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Services.Common.Abstract;

namespace Domain.Models;

public class ShoppingCart : BaseModel<int>
{
    [Range(minimum: 1, maximum: 100, ErrorMessage = "Please Select A Count Between 1 And 100")]
    public uint Count { get; set; }
    public int MenuItemId { get; set; }
    [ForeignKey(nameof(MenuItemId))]
    //[NotMapped]
    [ValidateNever]
    public virtual MenuItem MenuItem { get; set; }
    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    //[NotMapped]
    [ValidateNever]
    public virtual ApplicationUser User { get; set; }
}
