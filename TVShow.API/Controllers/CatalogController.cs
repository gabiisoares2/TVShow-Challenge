using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TVShow.Domain.ViewModel;
using TVShow.Service.Interfaces.Service;

namespace TVShow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalog([FromQuery] TvShowByFilterVM request, CancellationToken cancellationToken)
        {
            var content = await _catalogService.GetAllTvShows(request, cancellationToken);
            if (content != null)
                return Ok(content);
            else
                return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalog(Guid id, CancellationToken cancellationToken)
        {
            var content = await _catalogService.GetTvShowsById(id, cancellationToken);
            if (content != null)
                return Ok(content);
            else
                return NoContent();
        }

        [HttpGet("favourites")]
        public async Task<IActionResult> GetAllFavouritesAsync(CancellationToken cancellationToken)
        {
            var content = await _catalogService.GetByFavourites(cancellationToken);
            if(content != null)
                return Ok(content);
            return NoContent();
        }

        [HttpPatch("add-favourite")]
        public async Task<IActionResult> AddFavouritesTvShow(Guid[] id, CancellationToken cancellationToken)
        {
            await _catalogService.AddFavouritesTvShow(id, cancellationToken);
            return Ok();
        }
        [HttpPatch("delete-favourite")]
        public async Task<IActionResult> DeleteFavouritesTvShow(Guid[] id, CancellationToken cancellationToken)
        {
            await _catalogService.DeleteTvShowsByIds(id, cancellationToken);
            return Ok();
        }
    }
}
