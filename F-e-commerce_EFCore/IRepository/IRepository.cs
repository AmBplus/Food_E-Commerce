using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Services.Common.Abstract;

namespace F_e_commerce_EFCore.IRepository;

public interface IRepository<T> : IDisposable
{
    ViewResult Add(T entity);
    ViewResult Remove(T entity);
    ViewResult RemoveRange(IEnumerable<T> entities);
    ViewResult AddRange(IEnumerable<T> entities);
    void SaveChanges();
    T GetBy(int id);
    T GetBy(Expression<Func<T, bool>>? filter = null);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetByFilter(Expression<Func<T, bool>>? filter = null);
    Task<ViewResult> AddAsync(T entity);
    Task<ViewResult> AddRangeAsync(IEnumerable<T> entities);
    Task SaveChangesAsync();
    Task<T?> GetByAsync(int id);
    Task<T?> GetByAsync(Expression<Func<T, bool>>? filter = null);
    Task<IEnumerable<T?>> GetAllAsync();
    Task<IEnumerable<T?>> GetByFilterAsync(Expression<Func<T, bool>>? filter = null);
    bool IsExit(Expression<Func<T, bool>> filter);
    Task<bool> IsExitAsync(Expression<Func<T, bool>> filter);

}