using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Experian.Soumith.Exercise.Models
{
    public class Album
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        public List<Photo> Photos {get; set;}

        public Album()
        {
            Photos = new List<Photo>(); //TODO: create seperate class for the api and the returned Album, using a conversion operator
        }
    }
}

