namespace Services.Common.Abstract.IRepository;

public interface IStringRepository<TEntity> : IBaseModel<string>
{
    TEntity? GetBy(string id, string include = null);
    public ViewResult Update(TEntity entity);
    public Task<ViewResult> UpdateAsync(TEntity entity);
    Task<TEntity?> GetByAsync(string id, string include = null);
}