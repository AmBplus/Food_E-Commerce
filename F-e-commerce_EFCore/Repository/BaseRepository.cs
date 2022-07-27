using System.Linq.Expressions;
using F_e_Resources;
using Microsoft.EntityFrameworkCore;
using Services.Common.Abstract;
using Services.Common.Abstract.IRepository;

namespace F_e_commerce_EFCore.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    // Ctor
    public BaseRepository(FECommerceContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }
    // Properties
    // Instance Of Database
    private FECommerceContext Context { get; set; }
    // Instance Of TEntity DbSet
    internal DbSet<TEntity> DbSet;
    public ViewResult Add(TEntity entity)
    {
        DbSet.Add(entity);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public ViewResult Remove(TEntity entity)
    {
        DbSet.Remove(entity);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public ViewResult RemoveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public ViewResult AddRange(IEnumerable<TEntity> entities)
    {
        DbSet.AddRange(entities);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public void SaveChanges()
    {
        Context.SaveChanges();
    }
 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>if filter Null , Return Null , Otherwise send by filter </returns>
    public TEntity? GetBy(Expression<Func<TEntity, bool>>? filter = null, string include = null)
    {
        IQueryable<TEntity> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include))
        {
            query = GetQuery(query, include);
        }
        if (filter != null)
        {
            return query.Where(filter).FirstOrDefault();
        }
        return null;
    }
    public IEnumerable<TEntity?> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string include = null)
    {
        IQueryable<TEntity> query = DbSet;
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
    public IEnumerable<TEntity?> GetByFilter(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        , string include = null)
    {
        IQueryable<TEntity> query = DbSet;
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
    public async Task<ViewResult> AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public async Task<ViewResult> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
        return ViewResult.GetViewResultSucceed(Messages.SucceedMessage);
    }
    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
 
    public async Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>>? filter = null, string include = null)
    {
        IQueryable<TEntity> query = DbSet;
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
    public async Task<IEnumerable<TEntity?>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string include = null)
    {
        IQueryable<TEntity> query = DbSet;
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
    public async Task<IEnumerable<TEntity?>> GetByFilterAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        , string include = null)
    {
        IQueryable<TEntity> query = DbSet;
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
            return await orderBy(query).ToListAsync();
        }
        return await query.ToListAsync();
    }

    public bool IsExit(Expression<Func<TEntity, bool>> filter)
    {
        return DbSet.Any(filter);
    }

    public async Task<bool> IsExitAsync(Expression<Func<TEntity, bool>> filter)
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
    /// <summary>
    /// Get Query Of Include Relations
    /// </summary>
    /// <param name="query"></param>
    /// <param name="include"></param>
    /// <returns>Set All Include Then Return Query</returns>
    /// <exception cref="NullReferenceException"></exception>
    private IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) throw new NullReferenceException(nameof(include));
        var NewArray = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var includes in NewArray)
        {
            query = query.Include(includes);
        }
        return query;
    }
    /// <summary>
    /// Send Entity And Message To Set Update And Then Get Result Of Action
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="message"></param>
    /// <returns></returns>
 

}