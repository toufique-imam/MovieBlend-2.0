using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBlend.Models
{
    public class TVData
    {
        public string media_type { get; set; }
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string first_air_date { get; set; }
        public int[] genre_ids { get; set; }
        public genre[] genres { get; set; }
        public string[] origin_country { get; set; }
        public int id { get; set; }
        public string original_name { get; set; }
        public string original_language { get; set; }
        public string name { get; set; }
        public string backdrop_path { get; set; }
        public double popularity { get; set; }
        public int vote_count { get; set; }
        public double vote_average { get; set; }
        
    }
}
