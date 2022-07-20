using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class MenuItem
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    public string Image { get; set; }
    [Required]
    public int ForeignKeyCategory { get; set; }
    [ForeignKey(nameof(ForeignKeyCategory))]
    public virtual Category Category { get; set; }
    [Required]
    public int ForeignKeyFoodType { get; set; }
    [ForeignKey(nameof(ForeignKeyFoodType))]
    public virtual FoodType FoodType { get; set; }
}