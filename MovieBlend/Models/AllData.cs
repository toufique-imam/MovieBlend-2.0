using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBlend.Models
{
    public class AllData
    {
        public MovieArray movieArray { get; set; }
        public TVArray tVArray { get; set; }
        public PeopleArray peopleArray { get; set; }
        public string search_data { get; set; }
    }
}
