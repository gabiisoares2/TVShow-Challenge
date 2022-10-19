using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.Entity
{
    public class Picture : BaseEntity
    {
        public Guid TvShowId { get; set; }
        public virtual TvShow TvShow { get; set; }
        public string Uri { get; set; }
    }
}
