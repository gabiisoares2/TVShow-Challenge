using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TVShow.Domain.Core.Interface
{
    public interface IRepositoryWrite<TEntity, TKey> : IDisposable where TEntity : class
    {
        void Delete(TEntity entity);
        void Delete(TKey id);
        TEntity Add(TEntity entity);
        ValueTask<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void Attach(TEntity entity);
    }
}
