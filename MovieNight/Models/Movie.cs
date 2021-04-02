using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieNight.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string ImdbId { get; set; }

        [Required]
        public string Title { get; set; }

        public int? Year { get; set; }

        public DateTime? Released { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }

        public string Actors { get; set; }

        public string Poster { get; set; }

        public decimal? ImdbRating { get; set; }

        public string Language { get; set; }

        public string Plot { get; set; }

        public string Country { get; set; }

        public virtual ICollection<ApplicationUser> Fans { get; set; }
    }
}