using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading;

namespace LivePoints.Repository.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {

        private readonly DbContext entities = null;
        private DbSet<T> _objectSet;
        public GenericRepository(DbContext _entities)
        {
            entities = _entities;
            _objectSet = entities.Set<T>();
        }
        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
        }

        public T Get(Func<T, bool> predicate)
        {
            return _objectSet.First(predicate);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return _objectSet.Where(predicate);
            }

            return _objectSet.AsEnumerable();
        }

        
        public T GetById(object id)
        {
           return _objectSet.Find(id);
        }

        public void Update(T entity)
        {
            _objectSet.Update(entity);
        }

        /// <summary>
        /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method default no-tracking query.</remarks>
        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                        bool disableTracking = true)
        {
            IQueryable<T> query = _objectSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _objectSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Inserts a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        public Task InsertAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _objectSet.AddAsync(entity, cancellationToken);

            // Shadow properties?
            //var property = _dbContext.Entry(entity).Property("Created");
            //if (property != null) {
            //property.CurrentValue = DateTime.Now;
            //}
        }
    }
}
