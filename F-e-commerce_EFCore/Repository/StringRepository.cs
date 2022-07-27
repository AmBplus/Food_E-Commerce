using F_e_Resources;
using Microsoft.EntityFrameworkCore;
using Services.Common.Abstract;
using Services.Common.Abstract.IRepository;

namespace F_e_commerce_EFCore.Repository;

public class StringRepository<TEntity> : BaseRepository<TEntity> , IStringRepository<TEntity> where TEntity : class, IBaseModel<string>
{
    public StringRepository(FECommerceContext context):base(context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }
    // Properties
    // Instance Of Database
    private FECommerceContext Context { get; set; }
    public string Id { get => null;
        set => throw new NullReferenceException();
    }

    // Instance Of TEntity DbSet
    internal DbSet<TEntity> DbSet;

    public TEntity? GetBy(string id, string include = null)
    {
        IQueryable<TEntity> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include))
        {
            query = GetQuery(query, include);
        }
        return query.FirstOrDefault(x => x.Id == id);
    }
    public async Task<TEntity?> GetByAsync(string id, string include = null)
    {
        IQueryable<TEntity> query = DbSet;
        if (!string.IsNullOrWhiteSpace(include))
        {
            query = GetQuery(query, include);
        }
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
    public ViewResult Update(TEntity entity)
    {
        var message = string.Empty;
        if (!IsExit(x => x.Id == entity.Id))
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(TEntity));
            return ViewResult.GetViewResultFailed(message);
        }
        return Update(entity, message);
    }

    public async Task<ViewResult> UpdateAsync(TEntity entity)
    {
        var message = string.Empty;
        if (!await IsExitAsync(x => x.Id == entity.Id))
        {
            message = string.Format(Messages.CantFindDatabaseMessage, nameof(TEntity));
            return ViewResult.GetViewResultFailed(message);
        }
        return Update(entity, message);
    }
    private ViewResult Update(TEntity entity, string message)
    {

        if (!(Context.Entry(entity).State == EntityState.Modified))
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        Context.Update(entity);
        message = string.Format(Messages.UpdatedFromDatabaseMessage, nameof(TEntity));
        return ViewResult.GetViewResultSucceed(message);
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