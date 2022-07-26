namespace Services.Common.Abstract;

public interface IBaseModel<T>
{
    public T Id { get; set; }
}