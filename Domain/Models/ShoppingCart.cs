using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services.Common.Abstract;

namespace Domain.Models;

public class ShoppingCart: BaseModel<int>
{
    public uint Count { get; set; }
    public int MenuItemId { get; set; }
    [ForeignKey(nameof(MenuItemId))]
    public virtual MenuItem MenuItem { get; set; }
    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; }
    public int test { get; set; }
}