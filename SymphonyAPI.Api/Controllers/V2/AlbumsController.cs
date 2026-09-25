using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SymphonyAPI.Application.DTOs;
using SymphonyAPI.Application.Features.Albums.Commands;
using SymphonyAPI.Application.Features.Albums.Queries;

namespace SymphonyAPI.Api.Controllers.V2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/albums")]
    [ApiVersion("2.0")]
    public class AlbumsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlbumsController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Fetches all registered albums.
        /// </summary>
        /// <returns>List of all registered albums' data.</returns>
        /// <response code="200">The request was handled successfully.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var albums = await _mediator.Send(new GetAllAlbumsQuery());
            return Ok(albums);
        }

        /// <summary>
        /// Fetches an album by it's ID number.
        /// </summary>
        /// <param name="id">ID number of the album.</param>
        /// <returns>Data of the album.</returns>
        /// <response code="200">The album was found successfully.</response>
        /// <response code="404">The album does not exist.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var album = await _mediator.Send(new GetAlbumByIdQuery(id));
            return album is null ? NotFound() : Ok(album);
        }

        /// <summary>
        /// Fetches all registered albums from an artist.
        /// </summary>
        /// <param name="artistId">ID number of the artist.</param>
        /// <returns>List of all matching albums' data.</returns>
        /// <response code="200">The request was handled successfully.</response>
        [HttpGet("artist/{artistId}")]
        public async Task<IActionResult> GetAllByArtist([FromRoute] int artistId)
        {
            var albums = await _mediator.Send(new GetAllAlbumsByArtistQuery(artistId));
            return Ok(albums);
        }

        /// <summary>
        /// Creates a new album.
        /// </summary>
        /// <remarks>
        /// Creates an album using the provided information.
        /// 
        /// Request fields:
        /// - ArtistId: ID of the album's artist.
        /// - Title: Title of the album.
        /// - CoverURL: URL to the cover of the album.
        /// - Genre: Genre of the album.
        /// - ReleaseDate: Release date of the album in ISO 8601 format (YYYY-MM-DD).
        /// - Publisher: Name of the album's publisher.
        /// </remarks>
        /// <param name="cmd">Data provided for the request.</param>
        /// <response code="201">The album was created successfully.</response>
        /// <response code="400">The data provided is invalid.</response>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAlbumCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        /// <summary>
        /// Updates the information of an album.
        /// </summary>
        /// <remarks>
        /// Updates an album using the provided information.
        /// 
        /// Request fields:
        /// - ArtistId: ID of the album's artist.
        /// - Title: Title of the album.
        /// - CoverURL: URL to the cover of the album.
        /// - Genre: Genre of the album.
        /// - ReleaseDate: Release date of the album in ISO 8601 format (YYYY-MM-DD).
        /// - Publisher: Name of the album's publisher.
        /// </remarks>
        /// <param name="id">ID number of the album to update</param>
        /// <param name="dto">Data provided for the request.</param>
        /// <response code="204">The album was updated successfully.</response>
        /// <response code="400">The data provided is invalid.</response>
        /// <response code="404">The album does not exist.</response>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAlbumDTO dto)
        {
            var command = new UpdateAlbumCommand(id, dto.ArtistId, dto.Title, dto.CoverURL, dto.Genre, dto.ReleaseDate, dto.Publisher);
            var success = await _mediator.Send(command);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes an album.
        /// </summary>
        /// <param name="id">ID number of the album.</param>
        /// <response code="204">The album was deleted successfully.</response>
        /// <response code="404">The album does not exist.</response>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var success = await _mediator.Send(new DeleteAlbumCommand(id));
            return success ? NoContent() : NotFound();
        }
    }
}
