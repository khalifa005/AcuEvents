using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.IRepositories;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNewNotificationFor(string userId)
        {
            return _context.UserNotifications
                .Where(c => c.UserId == userId && !c.IsRead)
                .Select(c => c.Notification)
                .Include(g => g.Gig.Artist)
                .ToList();
        }



        public IEnumerable<UserNotification> MarkAsRead(string userId)
        {
           return _context.UserNotifications
                .Where(c => c.UserId == userId && !c.IsRead)
                .ToList();  
        }


    }
}