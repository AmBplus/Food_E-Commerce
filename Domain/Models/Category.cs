using System.ComponentModel.DataAnnotations;
using Services.Common.Abstract;

namespace Domain.Models;

public class Category : BaseModel<int>
{
    [Required]
    public string Name { get; set; }
    public uint DisplayOrder { get; set; }
}

