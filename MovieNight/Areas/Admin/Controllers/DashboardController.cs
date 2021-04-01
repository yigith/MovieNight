using MovieNight.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieNight.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            // SQL: SELECT Year, COUNT(*) AS Count
            // FROM Movies GROUP BY Year ORDER BY Year
            List<MovieYearCount> movieYearCounts =
                db.Movies
                .GroupBy(x => x.Year)
                .Select(g => new MovieYearCount()
                {
                    Year = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ToList();

            DashboardViewModel vm = new DashboardViewModel()
            {
                MovieYearCounts = movieYearCounts
            };
            return View(vm);
        }
    }
}