using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public partial class Event
    {
        public Event()
        {
            EventUsers = new HashSet<EventUser>();
        }

        public virtual int EventId { get; set; }
        public virtual int UserId { get; set; }
        [Display(Name = "Event Name")]
        public virtual string EventName { get; set; }
        [Display(Name = "Description")]
        public virtual string EventDescription { get; set; }
        [Display(Name = "Date & Time")]
        public virtual DateTime EventDateTime { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<EventUser> EventUsers { get; set; }
    }
}
