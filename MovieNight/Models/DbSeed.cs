using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MovieNight.Models
{
    public static class DbSeed
    {
        public static void SeedRolesAndUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                if (!context.Roles.Any(x => x.Name == "admin"))
                {
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    roleManager.Create(new IdentityRole("admin"));
                }

                if (!context.Users.Any(x => x.UserName == "admin@kod.fun"))
                {
                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    var adminUser = new ApplicationUser()
                    {
                        UserName = "admin@kod.fun",
                        Email = "admin@kod.fun",
                        EmailConfirmed = true
                    };
                    userManager.Create(adminUser, "P@ssword1");
                    userManager.AddToRole(adminUser.Id, "admin");
                }
            }
        }

        public async static Task SeedMovies()
        {
            using (var db = new ApplicationDbContext())
            using (var client = new HttpClient())
            {
                if (db.Movies.Any())
                    return;

                // http://www.omdbapi.com/?apikey=32620c90&i=tt0012348
                client.BaseAddress = new Uri("http://www.omdbapi.com/");

                foreach (string imdbId in ImdbMovieIds())
                {
                    var result = await client.GetAsync("?apikey=32620c90&i=" + imdbId);

                    if (result.IsSuccessStatusCode)
                    {
                        var dto = await result.Content.ReadAsAsync<MovieDto>();

                        Movie movie = new Movie()
                        {
                            Actors = ConvertNA(dto.Actors),
                            Country = ConvertNA(dto.Country),
                            Director = ConvertNA(dto.Director),
                            Genre = ConvertNA(dto.Genre),
                            Language = ConvertNA(dto.Language),
                            Plot = ConvertNA(dto.Plot),
                            Poster = ConvertNA(dto.Poster),
                            Title = ConvertNA(dto.Title),
                            Released = isNA(dto.Released) ? null as DateTime? : Convert.ToDateTime(dto.Released),
                            Year = isNA(dto.Year) ? null as int? : Convert.ToInt32(dto.Year),
                            ImdbId = ConvertNA(dto.ImdbId),
                            ImdbRating = isNA(dto.ImdbRating) ? null as decimal? : Convert.ToDecimal(dto.ImdbRating)
                        };
                        db.Movies.Add(movie);
                        db.SaveChanges();
                    }
                }

            }
        }

        public static string ConvertNA(string str)
        {
            return isNA(str) ? null : str;
        }

        public static bool isNA(string str)
        {
            return str == "N/A";
        }

        public static string[] ImdbMovieIds()
        {
            return "tt0111161,tt0068646,tt0468569,tt0071562,tt0050083,tt0167260,tt0110912,tt0108052,tt1375666,tt0137523,tt0120737,tt0109830,tt0060196,tt0167261,tt0133093,tt0099685,tt0080684,tt0073486,tt6751668,tt0816692,tt0317248,tt0245429,tt0120815,tt0120689,tt0118799,tt0114369,tt0102926,tt0076759,tt0056058,tt0047478,tt0038650,tt8503618,tt2582802,tt1675434,tt0482571,tt0407887,tt0253474,tt0172495,tt0120586,tt0114814,tt0110413,tt0110357,tt0103064,tt0095765,tt0095327,tt0088763,tt0064116,tt0054215,tt0034583,tt0027977,tt0021749,tt8267604,tt7286456,tt5311514,tt5074352,tt4633694,tt4154796,tt4154756,tt2380307,tt1853728,tt1345836,tt1187043,tt0986264,tt0910970,tt0405094,tt0364569,tt0209144,tt0119698,tt0087843,tt0082971,tt0081505,tt0078788,tt0078748,tt0057565,tt0057012,tt0051201,tt0050825,tt0047396,tt0043014,tt0032553,tt2106476,tt0361748,tt0338013,tt0180093,tt0169547,tt0119217,tt0114709,tt0112573,tt0105236,tt0090605,tt0086879,tt0086190,tt0082096,tt0062622,tt0052357,tt0045152,tt0040522,tt0033467,tt0022100,tt0012349".Split(',');
        }
    }


    public class MovieDto
    {
        public string ImdbId { get; set; }

        public string Title { get; set; }

        public string Year { get; set; }

        public string Released { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }

        public string Actors { get; set; }

        public string Poster { get; set; }

        public string ImdbRating { get; set; }

        public string Language { get; set; }

        public string Plot { get; set; }

        public string Country { get; set; }
    }
}