using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModel;
using GigHub.Persistence;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {

        
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork iUnitOfWork)
        {
            //_unitOfWork = new UnitOfWork(new ApplicationDbContext());
            _unitOfWork = iUnitOfWork;
        }
        
        // GET: Gigs
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Search(HomeViewModel vModel)
        {

            return RedirectToAction("Index","Home",new{query=vModel.SearchTerm});
        }


        //[HttpGet]
        //public ActionResult getJson()
        //{
        //    var result = _context.Users.Select(s => new
        //    {
        //        name = s.Name,
        //        id = s.Id
        //    }).Distinct().ToList();

        //    //var gresult = _context.Genres.Select(s => new
        //    //{
        //    //    name = s.Name,
        //    //    id = s.Id
        //    //}).Distinct().ToList();

        //    int conut=0;
        //    var list = new List<HomeViewModel>();
        //    foreach (var item in result)
        //    {
        //        conut = _context.Gigs.Where(s => s.ArtistId == item.id).Select(s => s.DateTime).Count();
        //        list.Add(new HomeViewModel() { name= item.name,count= conut });
        //    }
        //    return Json(list,JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult getJsonforColumns()
        //{
        //    var result = _context.Users.Select(s => new
        //    {
        //        name = s.Name,
        //        id = s.Id
        //    }).Distinct().ToList();

        //    var gresult = _context.Genres.Select(s => new
        //    {
        //        name = s.Name,
        //        id = s.Id
        //    }).Distinct().ToList();

        //    var list = new List<HomeViewModel>();
        //    foreach (var item in result)
        //    {
        //       var t = _context.Gigs.Where(s => s.ArtistId == item.id).Select(s =>s.GenreId).ToList();
               
        //        list.Add(new HomeViewModel() { name= _context.Gigs.Where(s => s.ArtistId == item.id).Select(d => d.Genre.Name).FirstOrDefault(),

        //        list = t });
        //    }
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Atendding()
        {
            var UserId = User.Identity.GetUserId();

            //what we got here is a list of attendances but when we add select .gig returen a list of gig
            var Gigs = _unitOfWork.Gigs.GetGigsUserAttending(UserId);

            //to protect from null refrence exception because we edit the viewModel
            //we didn't include ilookup because if we want to use it somewhere else because i lookup need i lookup obj in viewModel

            var attendences=_unitOfWork.AttendanceRepository.GetFutureAttendances(UserId).ToLookup(a=>a.GigId);
                

            //so the current return type is Gig Model but here we make it Goes To Home ViewMOdel ==Gig ViewModel
            var viewModel = new HomeViewModel
            {
                Heading = "Events I'm Going ",
                UpcomingGig = _unitOfWork.Gigs.GetGigsUserAttending(UserId),
                //UpcomingGig = Gigs,
                ShowAction = User.Identity.IsAuthenticated,
                //attendences=attendences
               attendences = _unitOfWork.AttendanceRepository.GetFutureAttendances(UserId).ToLookup(a=>a.GigId)
            };
            //here we added the magic string attr so we can redirect it to gigs in the shared view
            return View("Gigs", viewModel);
        }

        

        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetGigsByArtist(userId);
            

            return View(gigs);
        }

        [Authorize]
        public ActionResult GigForm()
        {
            //chane viemodel to gigform
            var ViewModel = new GigsViewModel
            {
                Heading="Add New Event",
                Genres = _unitOfWork.Gigs.GetGenres()
            };
            return View(ViewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (GigsViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewModel.Genres = _unitOfWork.Gigs.GetGenres();
                return View("GigForm", ViewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = ViewModel.GetDateTime(),
                Venue = ViewModel.Venue,
                GenreId = ViewModel.Genre
            };


            _unitOfWork.Gigs.Add(gig);

            _unitOfWork.Complete();

            //_context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }




        [Authorize]

        public ActionResult Edit(int gigId)
        {
            

            //var Gig = _context.Gigs.Single(c=>c.Id==id && c.ArtistId == UserId); 
            var Gig = _unitOfWork.Gigs.GetGigsDeatilsWithArtistAndGenre(gigId);
            if (Gig == null)
                return HttpNotFound();

            if ( Gig.ArtistId != User.Identity.GetUserId() )
                return new HttpUnauthorizedResult();

            var ViewModel = new GigsViewModel
            {

                //daynamic heading for gigform 
                Heading = "Edit Event",

                Id=Gig.Id,
                Genres = _unitOfWork.Gigs.GetGenres(),
                Date=Gig.DateTime.ToString("d MMM yyy"),
                Time= Gig.DateTime.ToString("HH:mm"),
                Venue=Gig.Venue,
                Genre=Gig.GenreId

            };

            return View("GigForm", ViewModel);
        }




        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigsViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewModel.Genres = _unitOfWork.Gigs.GetGenres().ToList();
                return View("GigForm", ViewModel);

            }

            var userId = User.Identity.GetUserId();

            //this is the part resposible for get the users will attend some events
            //.Include(g=>g.Attendances.Select(l=>l.Attendee))

            //var gig = _context.Gigs.Include(g=>g.Attendances.Select(l=>l.Attendee))
            //    .Single(c => c.Id == ViewModel.Id && c.ArtistId == userId);

            var gig = _unitOfWork.Gigs.GetGigWithAttende(ViewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != userId)
                return new HttpUnauthorizedResult();



            gig.Update(ViewModel.GetDateTime(),ViewModel.Genre,ViewModel.Venue);

            

            _unitOfWork.Complete();
            //_context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }


        public ActionResult Deatils(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigsDeatilsWithArtistAndGenre(id);

            if (gig == null)
                return HttpNotFound();

            var model = new GigDeatilsViewModel
            {
            //    ShowAction =User.Identity.IsAuthenticated,
            //    Heading="Event Deatils",
                Gig = gig
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                model.IsAttending =
                    _unitOfWork.AttendanceRepository.GetAttendance(gig.Id, userId) != null ;

                //model.IsFollowing = _context.Followings
                //    .Any(a=>a.FolloweeId==gig.ArtistId &&
                //         a.FollowerId==userId);
                
                //after repo
                model.IsFollowing = 
                    _unitOfWork.FollowingRepository.GetFollowing(userId, gig.ArtistId) != null ;
            }
            return View("Deatils",model);
        }





    }
}