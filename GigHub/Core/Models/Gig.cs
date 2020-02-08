using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }
        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public String ArtistId { get; set; }

        public DateTime DateTime { get; set; }


        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }
        [Required]
        public Byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            //assign the type of the notification and other prop to notfi obj
            //we refactor it to throw this as constructor resposibility
            var notification = Notification.GigCanceled( this);
            
            //return all users that attend the event
            //var attendes=_context.Attendances
            //    .Where(c=>c.GigId==gig.Id)
            //    .Select(s=>s.Attendee).ToList();

            //All code abpve replaced by this.Attendances.Select(l=>l.Attendee)

            foreach (var attende in Attendances.Select(l=>l.Attendee))
            {
              
                attende.Notify(notification);
                
            }

        }

        public void Update(DateTime dateTime, byte genre, string venue)
        {
            var notification =  Notification.GigUpdated( this ,DateTime,Venue);
            //this way i can keep track to orignal date and venue
            //but we chaneged it to privite
            //notification.OrignalDateTime = DateTime;
            //notification.OrignalVenue = Venue;
            
            //new date and venue
            DateTime =dateTime ;
            GenreId = genre;
            Venue = venue;
            
            foreach (var attende in Attendances.Select(l=>l.Attendee))
            {
              
                attende.Notify(notification);
                
            }
        }


        public void Create()
        {
            var notification = Notification.GigCreated(this);

            foreach (var attende in Attendances.Select(l=>l.Attendee))
            {
              
                attende.Notify(notification);
                
            }
        }
    }
}