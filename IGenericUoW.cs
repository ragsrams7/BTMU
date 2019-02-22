using LivePoints.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivePoints.Repository.UoW
{
    public interface IGenericUoW : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;

        //IRepository<T> ModelRepository { get; }

        void SaveChanges();

        int ExecuteSqlCommand(string sql, params object[] parameters);
    }
}
