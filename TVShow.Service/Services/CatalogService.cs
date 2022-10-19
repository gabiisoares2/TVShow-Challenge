using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShow.Domain.ViewModel;
using TVShow.Service.Interfaces.Repository;
using TVShow.Service.Interfaces.Service;

namespace TVShow.Service.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ITvShowRespository _tvShowRespository;
        private readonly IMapper _mapper;

        public CatalogService(ITvShowRespository tvShowRespository, IMapper mapper)
        {
            _tvShowRespository = tvShowRespository;
            _mapper = mapper;
        }

        public async Task AddFavouritesTvShow(Guid[] id, CancellationToken cancellationToken)
        {
            await _tvShowRespository.AddFavouritesTvShow(id, cancellationToken);
        }

        public async Task DeleteTvShowsByIds(Guid[] id, CancellationToken cancellationToken)
        {
            await _tvShowRespository.DeleteTvShowsByIds(id, cancellationToken);
        }

        public async Task<PaginatedItemsViewModel<TvShowByFilterResponseVM>> GetAllTvShows(TvShowByFilterVM request, CancellationToken cancellationToken)
        {
            (long count, IEnumerable<TvShowByFilterResponseVM> models) = await _tvShowRespository.GetAllByFilter(request, cancellationToken);
            return new PaginatedItemsViewModel<TvShowByFilterResponseVM>(request.PageNumber, request.PageSize, count, models);
        }

        public async Task<TvShowByFilterResponseVM> GetTvShowsById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _tvShowRespository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<TvShowByFilterResponseVM>(response);
        }

        public async Task<TvShowByFilterResponseVM> GetByFavourites(CancellationToken cancellationToken)
        {
            var response = await _tvShowRespository.GetByFavouritesAsync(cancellationToken);
            return _mapper.Map<TvShowByFilterResponseVM>(response);
        }
    }
}
