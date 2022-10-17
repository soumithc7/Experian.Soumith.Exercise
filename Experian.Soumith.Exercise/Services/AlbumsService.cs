using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Experian.Soumith.Exercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Experian.Soumith.Exercise.Services
{

    public class AlbumsService : IAlbumsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AlbumsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Controller method to get all albums
        /// </summary>
        public async Task<IEnumerable<Album>> Get()
        {
            var albums = await QueryAlbums(null);
            var photos = await QueryPhotos(albums.Select(album => album.Id)); //TODO note that there is a limit to how many album ids can be used

            return AddPhotosToAlbums(albums, photos);

        }

        /// <summary>
        /// Controller method to get all albums by user id
        /// </summary>
        public async Task<IEnumerable<Album>> Get(string userId)
        {
            var albums = await QueryAlbums(Int32.Parse(userId)); // TODO: Change to TryParse
            var photos = await QueryPhotos(albums.Select(album => album.Id)); //TODO note that there is a limit to how many album ids can be used

            return AddPhotosToAlbums(albums, photos);
        }

        /// <summary>
        /// Helper for Get() methods
        /// </summary>
        private List<Album> AddPhotosToAlbums(IEnumerable<Album> albums, IEnumerable<Photo> photos)
        {
            var accessibleAlbums = albums.ToDictionary(album => album.Id, album => album);

            foreach (Photo photo in photos)
            {
                accessibleAlbums[photo.AlbumId].Photos.Add(photo); // TODO check for when key doesnt exist
            }

            return new List<Album>(accessibleAlbums.Values);
        }

        /// <summary>
        /// Queries external api to retrieve albums
        /// </summary>
        private async Task<IEnumerable<Album>> QueryAlbums(int? userId)
        {
            var client = _httpClientFactory.CreateClient("jsonplaceholder");

            try
            {
                var response = userId.HasValue ? await client.GetAsync(QueryHelpers.AddQueryString("albums", "userId", userId.ToString())) :
                                 await client.GetAsync("albums");
                response.EnsureSuccessStatusCode();
                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<IEnumerable<Album>>(responseStream);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Queries external api to retrieve photos given a list of album ids
        /// </summary>
        private async Task<IEnumerable<Photo>> QueryPhotos(IEnumerable<int> albumIds)
        {
            var uri = albumIds.Aggregate("photos", (uri, currentAlbumId) => QueryHelpers.AddQueryString(uri, "albumId", currentAlbumId.ToString())); // TODO: extract magic string
            var client = _httpClientFactory.CreateClient("jsonplaceholder"); // TODO: extract magic string

            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<IEnumerable<Photo>>(responseStream);
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
    }
}

