using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SymphonyAPI.Application.DTOs;
using SymphonyAPI.Application.Features.Artists.Commands;
using SymphonyAPI.Application.Features.Artists.Queries;

namespace SymphonyAPI.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/artists")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class ArtistsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ArtistsController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Fetches all registered artists.
        /// </summary>
        /// <returns>List of all registered artists' data.</returns>
        /// <response code="200">The request was handled successfully.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var artists = await _mediator.Send(new GetAllArtistsQuery());
            return Ok(artists);
        }

        /// <summary>
        /// Fetches an artist by it's ID number.
        /// </summary>
        /// <param name="id">ID number of the artist.</param>
        /// <returns>Data of the artist.</returns>
        /// <response code="200">The artist was found successfully.</response>
        /// <response code="404">The artist does not exist.</response>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "ApiKey")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var artist = await _mediator.Send(new GetArtistByIdQuery(id));
            return artist is null ? NotFound() : Ok(artist);
        }

        /// <summary>
        /// Creates a new artist.
        /// </summary>
        /// <remarks>
        /// Creates an artist using the provided information.
        /// 
        /// Request fields:
        /// - Name: Name of the artist.
        /// - PhotoURL: URL to a photo of the artist, preferably in a square format.
        /// - Bio: Short description about the artist.
        /// </remarks>
        /// <param name="cmd">Data provided for the request.</param>
        /// <response code="201">The artist was created successfully.</response>
        /// <response code="400">The data provided is invalid.</response>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateArtistCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        /// <summary>
        /// Updates the information of an artist.
        /// </summary>
        /// <remarks>
        /// Updates an artist using the provided information.
        /// 
        /// Request fields:
        /// - Name: Name of the artist.
        /// - PhotoURL: URL to a photo of the artist, preferably in a square format.
        /// - Bio: Short description about the artist.
        /// </remarks>
        /// <param name="id">ID number of the artist to update</param>
        /// <param name="dto">Data provided for the request.</param>
        /// <response code="204">The artist was updated successfully.</response>
        /// <response code="400">The data provided is invalid.</response>
        /// <response code="404">The artist does not exist.</response>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateArtistDTO dto)
        {
            var command = new UpdateArtistCommand(id, dto.Name, dto.PhotoURL, dto.Bio);
            var success = await _mediator.Send(command);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes an artist.
        /// </summary>
        /// <param name="id">ID number of the artist.</param>
        /// <response code="204">The artist was deleted successfully.</response>
        /// <response code="404">The artist does not exist.</response>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var success = await _mediator.Send(new DeleteArtistCommand(id));
            return success ? NoContent() : NotFound();
        }
    }
}
