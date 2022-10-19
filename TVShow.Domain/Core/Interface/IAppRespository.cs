using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.Core.Interface
{
    public interface IAppRespository<TEntity, TKey> : IRepositoryRead<TEntity, TKey>, IRepositoryWrite<TEntity, TKey> where TEntity : class
    {
    }
}
