using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order=1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order=2)]
        public int NotificationId { get; set; }

        public bool IsRead { get;private set; }
        //we changed set to privite because the initialization was in tthe consrouctor
        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }
        
        //constructor to be used by Ef And protected So no other class can use it
        protected UserNotification()
        {
            
        }
        //protect from null exception
        public UserNotification(ApplicationUser user,Notification notification)
        {
            if(user == null)
                throw  new ArgumentNullException("user");

            if(notification == null)
                throw  new ArgumentNullException("notification");
            
            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}