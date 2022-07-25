using System.ComponentModel.DataAnnotations;

namespace Services.Common.Abstract;

public class BaseModel<T>
{
    [Key]
    public T Id { get; set; }
}