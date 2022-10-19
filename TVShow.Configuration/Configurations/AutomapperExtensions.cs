using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TVShow.Domain.Entity;
using TVShow.Domain.ViewModel;

namespace TVShow.Configuration.Configurations
{
    public static class AutomapperExtension
    {
        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutomapperExtension));
        }
    }

    public class ApplicationServiceAutoMapperProfile : Profile
    {
        public ApplicationServiceAutoMapperProfile()
        {
            CreateMap<CreateUserVM, IdentityUser<Guid>>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.ConfirmPassword));

            CreateMap<GetDataEpisodeDate.EpisodeDto, Episodes>();
            CreateMap<GetDataEpisodeDate.Tvshow, TvShow>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Cod, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom((src, dest, destMember, context) => src.Genres == null ? "" : JsonSerializer.Serialize(src.Genres.ToList())))
                .ForMember(dest => dest.Pictures, opt => opt.MapFrom(scr => ConverterPicture(scr.Pictures)))
                .ForMember(dest => dest.Episodes, opt => opt.MapFrom(scr => scr.Episodes));

            CreateMap<PictureVM, Picture>()
                .ForMember(dest => dest.TvShowId, opt => opt.MapFrom(dest => dest.TvShowId));
            CreateMap<Picture, PictureVM>()
                                .ForMember(dest => dest.TvShowId, opt => opt.MapFrom(dest => dest.TvShowId));

            CreateMap<EpisodeVM, Episodes>()
                                .ForMember(dest => dest.TvShowId, opt => opt.MapFrom(dest => dest.TvShowId));
            CreateMap<Episodes, EpisodeVM>()
                                .ForMember(dest => dest.TvShowId, opt => opt.MapFrom(dest => dest.TvShowId));

            CreateMap<TvShowByFilterVM, TvShow>();
            CreateMap<TvShowByFilterResponseVM, TvShow>();
            CreateMap<TvShow, TvShowByFilterResponseVM>();

        }

        private List<Picture> ConverterPicture(List<string>? strings)
        {
            if (strings == null) return null;
            var result = new List<Picture>();
            foreach (var item in strings)
            {
                result.Add(new Picture()
                {
                    Uri = item
                });
            }
            return result;
        }
    }
}
