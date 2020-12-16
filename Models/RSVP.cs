
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class RSVP
    {
        [Key]
        public int RSVPID {get;set;}
        public int UserID {get;set;}
        public int WeddingID {get;set;}
        public User GuestList {get;set;}
        public Wedding Wedding {get;set;}
    }
}