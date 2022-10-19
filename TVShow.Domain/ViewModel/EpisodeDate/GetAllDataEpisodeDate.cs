using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.ViewModel.EpisodeDate
{
    public class GetAllDataEpisodeDate
    {
        public string Total { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
        public Tv_Shows[] Tv_shows { get; set; }
    }

    public class Tv_Shows
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Permalink { get; set; }
        public string Start_date { get; set; }
        public object End_date { get; set; }
        public string Country { get; set; }
        public string Network { get; set; }
        public string Status { get; set; }
        public string Image_thumbnail_path { get; set; }
    }
}
