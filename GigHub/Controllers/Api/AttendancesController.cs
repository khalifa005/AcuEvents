using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        
        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendancesDto dto)
        {
             var UserId = User.Identity.GetUserId();
            var attendance = _unitOfWork.AttendanceRepository.GetAttendance(dto.GigId, UserId);
            if (attendance != null )
            {
                return BadRequest("the attendance already exist");
            }

             attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = UserId
            };

            _unitOfWork.AttendanceRepository.Add(attendance);

            _unitOfWork.Complete();

            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult RemoveAttende(int id)
        { 
            var UserId=User.Identity.GetUserId();

            var attendance=_unitOfWork.AttendanceRepository.GetAttendance(id,UserId);
            if (attendance == null)
            {
                return NotFound();
            }

            _unitOfWork.AttendanceRepository.Remove(attendance);
            _unitOfWork.Complete();
            
            return Ok(id);

        }

    }
}
