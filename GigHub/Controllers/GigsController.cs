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
            

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                Venue = ViewModel.Venue,
                GenreId = ViewModel.Genre,
                DateTime = ViewModel.DateTime
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}