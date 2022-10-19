using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.ViewModel
{
    public class PaginatedItemsViewModel<TEntity> where TEntity : class
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<TEntity> Rows { get; private set; }

        public PaginatedItemsViewModel(int pageNumber, int pageSize, long count, IEnumerable<TEntity> data)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Count = count;
            this.Rows = data;
        }
    }
}
