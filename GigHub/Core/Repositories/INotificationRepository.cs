using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.IRepositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotificationFor(string userId);

         IEnumerable<UserNotification> MarkAsRead(string userId);
    }
}