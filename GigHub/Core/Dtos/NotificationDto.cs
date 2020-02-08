using System;
using GigHub.Core.Models;

namespace GigHub.Core.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get;private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OrignalDateTime { get;private set; }
        public string OrignalVenue { get;private set; }

        public GigDto Gig { get; private set; }



    }
}