using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShow.Domain.Core.Interface;
using TVShow.Domain.Entity;
using TVShow.Domain.ViewModel;

namespace TVShow.Service.Interfaces.Repository
{
    public interface ITvShowRespository : IRepositoryRead<TvShow, Guid>, IRepositoryWrite<TvShow, Guid>
    {
        Task AddRanges(IList<TvShow> tvShows);

        Task<(long count, IEnumerable<TvShowByFilterResponseVM> models)> GetAllByFilter(TvShowByFilterVM tvShowByFilterVM, CancellationToken cancellationToken);
        Task<TvShow> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddFavouritesTvShow(Guid[] id, CancellationToken cancellationToken);
        Task DeleteTvShowsByIds(Guid[] id, CancellationToken cancellationToken);
        Task<IEnumerable<TvShow>> GetByFavouritesAsync(CancellationToken cancellationToken);
    }
}
