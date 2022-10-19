using AutoMapper;
using Microsoft.Extensions.Configuration;
using TVShow.Domain.Core;
using TVShow.Domain.Entity;
using TVShow.Domain.ViewModel;
using TVShow.Domain.ViewModel.EpisodeDate;
using TVShow.Service.Interfaces.ExternalServices;
using TVShow.Service.Interfaces.Repository;

namespace TVShow.Service.ExternalServices
{
    public class EpisodeDate : IEpisodeDate
    {
        private readonly IConfiguration _configuration;
        private readonly ITvShowRespository _tvShowRespository;
        private readonly IMapper _mapper;
        static string uri;

        public EpisodeDate(IConfiguration configuration, ITvShowRespository tvShowRespository, IMapper mapper)
        {
            _configuration = configuration;
            uri = _configuration.GetSection("EpisodeDateUri").Value;
            _tvShowRespository = tvShowRespository;
            _mapper = mapper;
        }

        public async Task ListAllEpisodes()
        {
            var lstEpisodes = await HttpHelper.Get<GetAllDataEpisodeDate>(string.Format("{0}/most-popular?page=1", uri));
            await AddRangeTVShow(lstEpisodes);
        }

        private async Task AddRangeTVShow(GetAllDataEpisodeDate lstEpisodes)
        {
            try
            {
                if (lstEpisodes != null && lstEpisodes.Tv_shows.Count() > 0)
                {
                    if (_tvShowRespository.GetAll().FirstOrDefault() != null)
                        return;
                    else
                    {
                        var tvShowEntity = new List<TvShow>();
                        foreach (var item in lstEpisodes.Tv_shows)
                        {
                            var data = await HttpHelper.Get<GetDataEpisodeDate>(string.Format("{0}/show-details?q={1}", uri, item.Id));
                            tvShowEntity.Add(_mapper.Map<TvShow>(data.TvShow));
                        }

                        if (tvShowEntity.Count > 0)
                        {
                           await _tvShowRespository.AddRanges(tvShowEntity);
                           await _tvShowRespository.SaveChangesAsync();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
