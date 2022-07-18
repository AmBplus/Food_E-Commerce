using System.ComponentModel.DataAnnotations;
using F_e_commerce_Constants;

namespace F_e_commerce_EFCore.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public uint DisplayOrder { get; set; }
}

