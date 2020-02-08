using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;

namespace GigHub.Controllers.Api
{
    public class NotificationController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetNotification()
        {
            var userId=User.Identity.GetUserId();
            var notfication = _unitOfWork
                .NotificationRepository
                .GetNewNotificationFor(userId);

            //will pass this notfication to view so when we call GetJSON we pass it as parameter
            return notfication.Select(Mapper.Map<Notification,NotificationDto>); 
        }



        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId=User.Identity.GetUserId();
            var notfication = _unitOfWork.NotificationRepository.MarkAsRead(userId);

            notfication.ForEach(n => n.Read());

            _unitOfWork.Complete();
            return Ok(); 
        }
    }
}
