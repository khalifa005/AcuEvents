using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistence;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]

        public IHttpActionResult Cancel(int id)
        {

            //get family event  check if it is the right family get all users will attende the event

            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGigWithAttende(id);

            if (gig == null)
                return NotFound();

            if (gig.IsCanceled)
            {
                return NotFound();
            }

            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
