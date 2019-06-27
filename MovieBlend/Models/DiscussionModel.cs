using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBlend.Models
{
    public class DiscussionModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Poster_ID { get; set; }
        [Required]
        public int Movie_ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string Movie_name { get; set; }
        public string poster_name { get; set; }
    }
}
