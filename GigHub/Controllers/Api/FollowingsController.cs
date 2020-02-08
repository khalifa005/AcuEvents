using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FolloersDto dto)
        {
            var userId = User.Identity.GetUserId();

            ////i had a big issue with this one befor?
            //if (_unitOfWork.FollowingRepository.GetFollowing(userId, dto.followeeId) ==null  //and it should be !=null A7a)
            //    return BadRequest("Following already exists.");


            if (_unitOfWork.FollowingRepository.GetFollowingBool(userId, dto.followeeId))
                return BadRequest("Following already exists.");

               var followViewModel = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.followeeId
            };

            _unitOfWork.FollowingRepository.Add(followViewModel);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var followe = _unitOfWork.FollowingRepository.GetFollowing(userId,id);
          
            if (followe == null)
                return NotFound();
            
            _unitOfWork.FollowingRepository.Remove(followe);
            _unitOfWork.Complete();

            return Ok(id);
        }




    }
}
