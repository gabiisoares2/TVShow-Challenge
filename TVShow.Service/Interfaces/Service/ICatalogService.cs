using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShow.Domain.ViewModel;

namespace TVShow.Service.Interfaces.Service
{
    public interface ICatalogService
    {
        Task<PaginatedItemsViewModel<TvShowByFilterResponseVM>> GetAllTvShows(TvShowByFilterVM request, CancellationToken cancellationToken);
        Task<TvShowByFilterResponseVM> GetTvShowsById(Guid id, CancellationToken cancellationToken);
        Task AddFavouritesTvShow(Guid[] id, CancellationToken cancellationToken);
        Task DeleteTvShowsByIds(Guid[] id, CancellationToken cancellationToken);
        Task<TvShowByFilterResponseVM> GetByFavourites(CancellationToken cancellationToken);
    }
}
