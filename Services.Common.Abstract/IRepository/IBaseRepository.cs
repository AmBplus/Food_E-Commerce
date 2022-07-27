using System.Linq.Expressions;

namespace Services.Common.Abstract.IRepository;

public interface IBaseRepository<TEntity>
{
    ViewResult Add(TEntity entity);
    ViewResult Remove(TEntity entity);
    ViewResult RemoveRange(IEnumerable<TEntity> entities);
    ViewResult AddRange(IEnumerable<TEntity> entities);
    void SaveChanges();
   
    TEntity GetBy(Expression<Func<TEntity, bool>>? filter = null, string include = null);
    IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string include = null);
    IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string include = null);
    Task<ViewResult> AddAsync(TEntity entity);
    Task<ViewResult> AddRangeAsync(IEnumerable<TEntity> entities);
    Task SaveChangesAsync();
    
    Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>>? filter = null, string include = null);
    Task<IEnumerable<TEntity?>> GetAllAsync(Func<IQueryable<TEntity?>, IOrderedQueryable<TEntity?>>? orderBy = null, string? include = null);
    Task<IEnumerable<TEntity?>> GetByFilterAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        , string include = null);
    bool IsExit(Expression<Func<TEntity, bool>> filter);
    Task<bool> IsExitAsync(Expression<Func<TEntity, bool>> filter);
    
}