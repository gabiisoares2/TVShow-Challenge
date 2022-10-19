using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TVShow.Domain.Core.Interface
{
    public interface IRepositoryRead<TEntity, TKey> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity Get(TKey id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, object>>[] includes, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        ValueTask<TEntity> GetAsync(TKey id);
        ValueTask<TEntity> GetAsync(TKey id, CancellationToken cancellationToken);
    }
}
