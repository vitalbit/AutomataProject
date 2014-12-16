using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using System.Data.Entity;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }
        public DbContext Context
        {
            get { return context; }
        }

        public void Commit()
        {
            if (context != null)
                context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}
