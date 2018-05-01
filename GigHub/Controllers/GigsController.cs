using GigHub.Models;
using GigHub.ViewModel;
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


        public ActionResult Create()
        {

            var ViewModel = new GigsViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(ViewModel);
        }
    }
}