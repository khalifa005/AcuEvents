using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Core.ViewModel;
using GigHub.Persistence;

namespace GigHub.Controllers
{
    public class DeanController : Controller
    {


        private ApplicationDbContext _context;

        public DeanController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Dean
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult getJson()
        {
            var result = _context.Users.Select(s => new
            {
                name = s.Name,
                id = s.Id
            }).Distinct().ToList();
            

            int conut = 0;
            var list = new List<DeanViewModel>();
            foreach (var item in result)
            {
                conut = _context.Gigs.Where(s => s.ArtistId == item.id).Select(s => s.DateTime).Count();
                list.Add(new DeanViewModel() { name = item.name, count = conut });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getJsonforColumns()
        {
            var result = _context.Users.Select(s => new
            {
                name = s.Name,
                id = s.Id

            }).Distinct().ToList();


            var list = new List<DeanViewModel>();
            foreach (var item in result)
            {
                var cateId = _context.Gigs.Where(s => s.ArtistId == item.id).Select(d => d.GenreId).ToList();
                 
                foreach (var category in cateId)
                {
                    var e = _context.Gigs.Where(s => s.ArtistId == item.id && s.GenreId == category).Select(s => new DeanViewModel()
                    {
                        name = _context.Gigs.Where(ss => ss.ArtistId == item.id && ss.GenreId == category).Select(d=>d.Genre.Name).FirstOrDefault(),
                        count = cateId.Count()
                    });

                }
              
                //list.Add(new DeanViewModel()
                //{
                //list = e
                //});
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }





        public ActionResult test()
        {
            var result = _context.Users.Select(s => new
            {
                name = s.Name,
                id = s.Id
            }).Distinct().ToList();

            var gresult = _context.Genres.Select(s => new
            {
                name = s.Name,
                id = s.Id
            }).Distinct().ToList();

            var list = new List<DeanViewModel>();
            foreach (var item in result)
            {

                list.Add(new DeanViewModel()
                {

                name = _context.Gigs.Where(s => s.ArtistId == item.id).Select(d => d.Genre.Name).FirstOrDefault(),

                    list = _context.Gigs.Where(s => s.ArtistId == item.id).ToList(),


                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}