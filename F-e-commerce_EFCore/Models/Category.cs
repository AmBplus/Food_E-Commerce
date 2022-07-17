using System.ComponentModel.DataAnnotations;

namespace F_e_commerce_EFCore.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
}

