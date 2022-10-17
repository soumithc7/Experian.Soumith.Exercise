using System;
using Experian.Soumith.Exercise.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Experian.Soumith.Exercise.Services
{
    public interface IAlbumsService
    {
        Task<IEnumerable<Album>> Get();
        Task<IEnumerable<Album>> Get(string userId);
    }
}

