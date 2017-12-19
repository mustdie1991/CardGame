using System;
using Microsoft.EntityFrameworkCore;

namespace CardGame.Db.UnitOfWork
{
    public class CommonUnitOfWork<TContext>: IDisposable where TContext : DbContext
    {
        private bool _isDisposed;

        public CommonUnitOfWork(TContext context)
        {
            this.Context = context;
        }

        public TContext Context { get; }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            if (!this._isDisposed)
            {
                this.Context.Dispose();
            }

            this._isDisposed = true;
        }
    }
}
