using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get;private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OrignalDateTime { get;private set; }
        public string OrignalVenue { get;private set; }


        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {
            
        }

        private Notification(NotificationType type , Gig gig)
        {
            if(gig == null)
                throw  new ArgumentNullException("gig");

            Type= type;
            Gig = gig;
            DateTime=DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated,gig);
        }


        public static Notification GigUpdated(Gig newGig,DateTime orignalDateTime,string orignalVenue)
        {
            var  notification= new Notification(NotificationType.GigUpdated,newGig);
            
            
            notification.OrignalDateTime = orignalDateTime;
            notification.OrignalVenue = orignalVenue;
            return notification;

        }


        
        public static Notification GigCanceled(Gig Gig)
        {
            return new Notification(NotificationType.GigCanceled,Gig);
            
        }
    }
}