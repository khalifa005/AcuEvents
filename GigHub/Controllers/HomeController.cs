using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModel;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;

namespace GigHub.Controllers
{
    
    public class HomeController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }
        public ActionResult Index(string SearchTerm=null)
        {
           var UpcomingGigs = _unitOfWork.Gigs.GetFutureGigsInDeatils(SearchTerm);

            

            var UserId=User.Identity.GetUserId();
            
            //before repository
            //var attendences=_context.Attendances
            //    .Where(a=>a.AttendeeId==UserId && a.Gig.DateTime>DateTime.Now)
            //    .ToList()
            //    .ToLookup(a=>a.GigId);

            //after repository

            //Going?
            var attendences=_unitOfWork.AttendanceRepository
                .GetFutureAttendances(UserId)
                .ToLookup(a=>a.GigId);

            
            var viewmodel = new HomeViewModel
            {
                Heading="All Events",
                UpcomingGig=UpcomingGigs,
                ShowAction=User.Identity.IsAuthenticated,
                SearchTerm = SearchTerm, 
                attendences=attendences
            };
            //here we added the magic string attr so we can redirect it to gigs in the shared view
            return View("Gigs", viewmodel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}