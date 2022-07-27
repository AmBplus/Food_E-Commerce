using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Services.Common.Abstract;

namespace Domain.Models;

public class OrderDetail : BaseModel<int>
{
    public string Name { get; set; }
    public int OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    [ValidateNever]
    public OrderHeader OrderHeader { get; set; }
    public int MenuItemId { get; set; }
    [ForeignKey(nameof(MenuItemId))]
    public MenuItem MenuItem { get; set; }
    [Required]
    public decimal Price { get; set; }
    public int Count { get; set; }

}