using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieNight.Areas.Admin.Controllers
{
    public class MoviesController : AdminBaseController
    {
        // GET: Admin/Movies
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }
    }
}