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
        [Required(ErrorMessage = "User is required.")]
        public virtual int UserId { get; set; }
        [Display(Name = "Event Name")]
        [Required(ErrorMessage = "Event Name is required.")]
        public virtual string EventName { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public virtual string EventDescription { get; set; }
        [Display(Name = "Date & Time")]
        public virtual DateTime EventDateTime { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<EventUser> EventUsers { get; set; }
    }
}
