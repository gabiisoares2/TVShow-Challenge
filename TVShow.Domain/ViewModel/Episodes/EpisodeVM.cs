using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.ViewModel
{
    public class EpisodeVM
    {
        public Guid TvShowId { get; set; }
        public int Season { get; set; }
        public int Episode { get; set; }
        public string Name { get; set; }
        public DateTime Air_date { get; set; }
    }
}
