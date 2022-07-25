using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace Domain.Dto;

public class MenuItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public  CategoryDto Category { get; set; }
    
    public  FoodTypeDto FoodType { get; set; }
}