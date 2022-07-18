using System.ComponentModel.DataAnnotations;

namespace F_e_commerce_EFCore.Models;

public class FoodType
{
    public  int Id { get; set; }
    [Required]
    public  string Name { get; set; }
}