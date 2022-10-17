using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Experian.Soumith.Exercise.Models;
using Experian.Soumith.Exercise.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Experian.Soumith.Exercise.Controllers
{
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumsService _albumsService;

        public AlbumsController(IAlbumsService albumService)
        {
            _albumsService = albumService;
        }

        [HttpGet]
        [Route("albums")]
        public async Task<IEnumerable<Album>> GetAllAlbums()
        {
            return await _albumsService.Get();
        }

        [HttpGet]
        [Route("{userId}/albums")]
        public async Task<IEnumerable<Album>> GetAlbumsByUserId(string userId)
        {
            return await _albumsService.Get(userId);
        }
    }
}

