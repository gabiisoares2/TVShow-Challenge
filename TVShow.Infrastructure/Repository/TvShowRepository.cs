using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShow.Domain.Entity;
using TVShow.Domain.Tools;
using TVShow.Domain.ViewModel;
using TVShow.Infrastructure.Context;
using TVShow.Infrastructure.Core;
using TVShow.Service.Interfaces.Repository;

namespace TVShow.Infrastructure.Repository
{
    public class TvShowRepository : AppRepository<TvShow, Guid>, ITvShowRespository
    {
        private readonly IMapper _mapper;
        public TvShowRepository(TVShowDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task AddFavouritesTvShow(Guid[] ids, CancellationToken cancellationToken)
        {
            foreach (var id in ids)
            {
                await Db.Database.ExecuteSqlInterpolatedAsync($@"update TvShow set isFavourite = 1 where id in ({id})");
            }
            await Db.SaveChangesAsync();
        }

        public async Task AddRanges(IList<TvShow> tvShows)
        {
            await Db.AddRangeAsync(tvShows);
        }

        public async Task DeleteTvShowsByIds(Guid[] ids, CancellationToken cancellationToken)
        {
            foreach (var id in ids)
            {
                await Db.Database.ExecuteSqlInterpolatedAsync($@"update TvShow set isFavourite = 0 where id in ({id})");
            }
            await Db.SaveChangesAsync();
        }

        public async Task<(long count, IEnumerable<TvShowByFilterResponseVM> models)> GetAllByFilter(TvShowByFilterVM request, CancellationToken cancellationToken)
        {
            try
            {
                var query = Db.TvShows
                                  .Include(i => i.Pictures)
                                  .Include(i => i.Episodes)
                                  .AsQueryable();

                if (!string.IsNullOrEmpty(request.OrderColumn))
                {
                    query = query.OrderByDynamic(request.OrderColumn, request.OrderDirection);
                }

                if (!string.IsNullOrEmpty(request.SearchGenre))
                    query = query.Where(x => x.Genres.Contains(request.SearchGenre));

                if (!string.IsNullOrEmpty(request.SearchByName))
                    query = query.Where(x => x.Name.Contains(request.SearchByName));

                if (!string.IsNullOrEmpty(request.SearchNetwork))
                    query = query.Where(x => x.Network.Contains(request.SearchNetwork));

                var skip = ((request.PageNumber - 1) * request.PageSize);
                var take = request.PageSize;
                var count = await query.LongCountAsync();

                var models = await query.Skip((skip)).Take(take).ToListAsync();
                List<TvShowByFilterResponseVM> lstModel = _mapper.Map<IEnumerable<TvShowByFilterResponseVM>>(models).ToList();

                return (count, lstModel);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    
        public async Task<TvShow> GetByIdAsync (Guid id, CancellationToken cancellationToken)
        {
            return await Db.TvShows
                    .Include(i => i.Pictures)
                    .Include(i => i.Episodes)
                    .Where(x => x.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TvShow>> GetByFavouritesAsync(CancellationToken cancellationToken)
        {
            return await Db.TvShows
                    .Include(i => i.Pictures)
                    .Include(i => i.Episodes)
                    .Where(x => x.IsFavourite).ToListAsync(cancellationToken);
        }


    }
}
