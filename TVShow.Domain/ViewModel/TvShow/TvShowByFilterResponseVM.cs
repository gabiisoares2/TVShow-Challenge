using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.ViewModel
{
    public class TvShowByFilterResponseVM
    {
        public Guid Id { get; set; }
        public int Cod { get; set; }
        public string Name { get; set; }
        public string Permalink { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Description_source { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime? End_date { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public int Runtime { get; set; }
        public string Network { get; set; }
        public string Youtube_link { get; set; }
        public string Image_path { get; set; }
        public string Image_thumbnail_path { get; set; }
        public string Rating { get; set; }
        public string Rating_count { get; set; }
        public string Countdown { get; set; }
        public string Genres { get; set; }
        public virtual List<PictureVM> Pictures { get; set; }
        public virtual List<EpisodeVM> Episodes { get; set; }
        public bool iSFavourite { get; set; }
    }
}
