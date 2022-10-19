using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.ViewModel
{
    public class TvShowByFilterVM : BaseEntityViewModel
    {
        public string? SearchGenre { get; set; }
        public string? SearchNetwork { get; set; }
        public string? SearchByName { get; set; }
    }
}
