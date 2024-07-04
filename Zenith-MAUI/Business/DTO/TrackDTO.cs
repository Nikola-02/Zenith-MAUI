using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith_MAUI.Business.DTO
{
    public class TrackDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string Image { get; set; }
        public string Audio { get; set; }
        public LookupDTO Album { get; set; }
        public LookupDTO Artist { get; set; }
        public LookupDTO Genre { get; set; }
        public LookupDTO MediaType { get; set; }
        public int LikesCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool? IsLiked { get; set; }
    }
}
