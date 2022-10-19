using System.Text.Json.Serialization;

namespace TVShow.Domain.ViewModel
{
    public class GetDataEpisodeDate
    {
        public Tvshow TvShow { get; set; }
        public class Tvshow
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Permalink { get; set; }
            public string Url { get; set; }
            public string Description { get; set; }
            public string Description_source { get; set; }
            public string Start_date { get; set; }
            public object End_date { get; set; }
            public string Country { get; set; }
            public string Status { get; set; }
            public int Runtime { get; set; }
            public string Network { get; set; }
            public object Youtube_link { get; set; }
            public string Image_path { get; set; }
            public string Image_thumbnail_path { get; set; }
            public string Rating { get; set; }
            public string Rating_count { get; set; }
            public object Countdown { get; set; }
            public List<string> Genres { get; set; }
            public List<string>? Pictures { get; set; }
            public EpisodeDto[] Episodes { get; set; }
        }

        public class EpisodeDto
        {
            public int Season { get; set; }
            [JsonPropertyName("Episode")]
            public int Episodes { get; set; }
            public string Name { get; set; }
            public string Air_date { get; set; }
        }
    }
}
