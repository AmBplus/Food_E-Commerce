using System.Linq.Expressions;
using Domain.Models;
using F_e_Resources;
using Microsoft.EntityFrameworkCore;
using Services.Common.Abstract;
using Services.Common.Abstract.IRepository;

namespace F_e_commerce_EFCore.Repository;

  public class Repository<T> :  IRepository<T> where T : BaseModel<int>
{
    // Ctor
    public Repository(FECommerceContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }
    // Properties
    // Instance Of Database
    private FECommerceContext Context { get; set; }
    // Instance Of T DbSet
    internal DbSet<T> DbSet ;
    public ViewResult Add(T entity)
    {
        DbSet.Add(entity);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);    
    }
    public ViewResult Remove(T entity)
    {
        DbSet.Remove(entity);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public ViewResult RemoveRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public ViewResult AddRange(IEnumerable<T> entities)
    {
        DbSet.AddRange(entities);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public void SaveChanges()
    {
        Context.SaveChanges();
    }
    public T? GetBy(int id, string include = null)
    {
        IQueryable<T> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include))
        {
            query = GetQuery(query, include);
        }
        return query.FirstOrDefault(x=>x.Id == id);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>if filter Null , Return Null , Otherwise send by filter </returns>
    public T? GetBy(Expression<Func<T, bool>>? filter = null, string include = null)
    {
        IQueryable<T> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include))
        {
            query = GetQuery(query, include);
        }
        if (filter != null)
        {
            return query.Where(filter).FirstOrDefault()  ;
        }
        return null;
    }
    public IEnumerable<T?> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,string include = null)
    {
        IQueryable<T> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include))
        {
            query = GetQuery(query, include);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        return DbSet.ToList();
    }
    public IEnumerable<T?> GetByFilter(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
        , string include = null)
    {
        IQueryable<T> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include))
        {
            query = GetQuery(query, include);
        }
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        return query.ToList();
    }
    public async Task<ViewResult> AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public async Task<ViewResult> AddRangeAsync(IEnumerable<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public async Task SaveChangesAsync()
    {
      await Context.SaveChangesAsync();
    }
    public async Task<T?> GetByAsync(int id, string include = null)
    {
        IQueryable<T> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include))
        {
            query = GetQuery(query, include);
        }
        return await query.FirstOrDefaultAsync(x=>x.Id == id);
    }
    public async Task<T?> GetByAsync(Expression<Func<T, bool>>? filter = null, string include = null)
    {
        IQueryable<T> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include))
        {
            query = GetQuery(query, include);
        }
        if (filter != null)
        {
            return await query.Where(filter).FirstOrDefaultAsync();
        }
        return null;
    }
    public async Task<IEnumerable<T?>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,string include = null)
    {
        IQueryable<T> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include))
        {
            query = GetQuery(query, include);
        }
        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        return await query.ToListAsync();
    }
    public async Task<IEnumerable<T?>> GetByFilterAsync(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
        , string include = null)
    {
        IQueryable<T> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include) )
        {
            query = GetQuery(query, include);
        }
        if (filter != null)
        {
             query = query.Where(filter);
        }
        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        return await query.ToListAsync();
    }

    public bool IsExit(Expression<Func<T, bool>> filter)
    {
        return DbSet.Any(filter);
    }

    public async Task<bool> IsExitAsync(Expression<Func<T, bool>> filter)
    {
        return await DbSet.AnyAsync(filter);
    }

    private bool IsDisposed = false;

    public void Dispose()
    { 
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool flag)
    {
        if (!IsDisposed)
        {
            if (flag)
            {
                Context.Dispose();
                IsDisposed = true;
            }
        }
    }

    private IQueryable<T> GetQuery(IQueryable<T> query, string include)
    {
        if(string.IsNullOrWhiteSpace(include)) throw new NullReferenceException(nameof(include));
        var NewArray = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var includes in NewArray)
        {
          query =  query.Include(includes);
        }
        return query;
    }

}

