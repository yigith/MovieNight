using Microsoft.AspNet.Identity;
using MovieNight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieNight.Controllers
{
    public class MoviesController : BaseController
    {
        [Authorize]
        public ActionResult Favorites()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(user.Favorites.ToList());
        }

        [HttpPost]
        [Authorize]
        // POST: Movies/AddToFavorites
        public ActionResult AddToFavorites(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null) return HttpNotFound();
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            user.Favorites.Add(movie);
            db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        [Authorize]
        // POST: Movies/RemoveFromFavorites
        public ActionResult RemoveFromFavorites(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null) return HttpNotFound();
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            user.Favorites.Remove(movie);
            db.SaveChanges();
            return Json(new { success = true });
        }

    }
}