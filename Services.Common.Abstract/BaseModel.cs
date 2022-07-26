using System.ComponentModel.DataAnnotations;

namespace Services.Common.Abstract;

public class BaseModel<T> : IBaseModel<T>
{
    [Key]
    public T Id { get; set; }
}