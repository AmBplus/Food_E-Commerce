using System.Linq.Expressions;

namespace Services.Common.Abstract.IRepository;

public interface IRepository<T> : IDisposable where T : BaseModel<int>
{
    ViewResult Add(T entity);
    ViewResult Remove(T entity);
    ViewResult RemoveRange(IEnumerable<T> entities);
    ViewResult AddRange(IEnumerable<T> entities);
    void SaveChanges();
    T? GetBy(int id, string include = null);
    T GetBy(Expression<Func<T, bool>>? filter = null, string include = null);
    IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,string include = null  );
    IEnumerable<T> GetByFilter(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string include = null);
    Task<ViewResult> AddAsync(T entity);
    Task<ViewResult> AddRangeAsync(IEnumerable<T> entities);
    Task SaveChangesAsync();
    Task<T?> GetByAsync(int id, string include = null);
    Task<T?> GetByAsync(Expression<Func<T, bool>>? filter = null, string include = null);
    Task<IEnumerable<T?>> GetAllAsync(Func<IQueryable<T?>, IOrderedQueryable<T?>>? orderBy= null ,string ? include  = null );
    Task<IEnumerable<T?>> GetByFilterAsync(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>,IOrderedQueryable<T>> orderBy = null
        ,string include = null);
    bool IsExit(Expression<Func<T, bool>> filter);
    Task<bool> IsExitAsync(Expression<Func<T, bool>> filter);
    
    

}