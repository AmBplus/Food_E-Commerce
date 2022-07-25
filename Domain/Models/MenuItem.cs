using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services.Common.Abstract;

namespace Domain.Models;

public class MenuItem : BaseModel<int>
{
    
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    public string Image { get; set; }
    [Required]
    [Display(Name = "Category")]
    public int ForeignKeyCategory { get; set; }
    [ForeignKey(nameof(ForeignKeyCategory))]
    public virtual Category Category { get; set; }
    [Required]
    [Display(Name = "FoodType")]
    public int ForeignKeyFoodType { get; set; }
    [ForeignKey(nameof(ForeignKeyFoodType))]
    public virtual FoodType FoodType { get; set; }
}