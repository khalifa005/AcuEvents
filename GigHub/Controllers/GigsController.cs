using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {

        private ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Gigs
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {

            var ViewModel = new GigsViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(ViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create (GigsViewModel ViewModel)
        {
            var ArtistId = User.Identity.GetUserId();
            var Artist = _context.Users.Single(u => u.Id == ArtistId);

            var GenreId = _context.Genres.Single(g => g.Id == ViewModel.Genre);
            var gig = new Gig
            {
                Artist = Artist,
                Venue = ViewModel.Venue,
                Genre = GenreId,
                DateTime = DateTime.Parse(string.Format("{0} {1}", ViewModel.Date, ViewModel.Time))
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}