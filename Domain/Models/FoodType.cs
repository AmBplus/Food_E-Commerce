using System.ComponentModel.DataAnnotations;
using Services.Common.Abstract;

namespace Domain.Models;

public class FoodType : BaseModel<int>
{
  
    [Required]
    public  string Name { get; set; }
}