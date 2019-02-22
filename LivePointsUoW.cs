using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivePoints.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace LivePoints.Repository.UoW
{
    public class LivePointsUoW<TContext> : IGenericUoWofT<TContext>, IGenericUoW where TContext : DbContext
    {
        //private readonly LivePointsDBContext entities = null;

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        //private readonly LivePointsDBContext _livePointsDBContext;// = null;
        private readonly TContext _context;

        public LivePointsUoW(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TContext DbContext => _context;
        //public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        //public IRepository<T> RepositoryFor<T>() where T : class
        //{
        //    if (repositories.Keys.Contains(typeof(T)) == true)
        //    {
        //        return repositories[typeof(T)] as IRepository<T>;
        //    }
        //    IRepository<T> repo = new GenericRepository<T>(_context);
        //    repositories.Add(typeof(T), repo);
        //    return repo;
        //}

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;

        public int ExecuteSqlCommand(string sql, params object[] parameters) => _context.Database.ExecuteSqlCommand(sql, parameters);


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(T);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new GenericRepository<T>(_context);
            }

            return (IRepository<T>)repositories[type];
        }

        //public Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IGenericUoW[] unitOfWorks)
        //{
        //    throw new NotImplementedException();
        //}




    }
}
