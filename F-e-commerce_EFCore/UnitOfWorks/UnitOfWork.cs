using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Common.Abstract.IUnitOfWorks;
namespace F_e_commerce_EFCore.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        protected internal UnitOfWork(FECommerceContext context)
        {
            Context = context;
        }

        private FECommerceContext Context { get; set; }
        public async Task BeginTrans()
        {
            await Context.Database.BeginTransactionAsync();
        }

        public async Task CommitTrans()
        {
            await Context.SaveChangesAsync();
            await Context.Database.CommitTransactionAsync();

        }

        public async Task RollBack()
        {
            await Context.Database.RollbackTransactionAsync();
        }

        public bool IsDisposed { get; protected set; }

        public async Task SaveChangesAsync()
        {
            _ = Context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async Task Dispose(bool disposing)
        {
            if (IsDisposed) return;
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                    IsDisposed = true;
                }
            }
        }
    }
}
