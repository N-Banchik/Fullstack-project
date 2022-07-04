using DataAccess.Data.Entities.Bridge_Entities;
using DataAccess.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Event : IPhotoble<Event>
    {


        public int Id { get; set; }
        public string? EventTitle { get; set; }
        public string? EventDescription { get; set; }
        public string? EventRules { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventCreated { get; set; } = DateTime.Now;
        public string? EventLocation { get; set; }
        public bool Canceled { get; set; }
        public bool passed { get; set;}
        public int EventCreatorId { get; set; }
        public User? Creator { get; set; }
        public int HobbyId { get; set; }
        public Hobby? Hobby { get; set; }

        public ICollection<UserEvent>? Users { get; set; }
        public ICollection<Photo<Event>>? Photos { get; set; }
        public ICollection<Post>? Posts { get; set; }

    }
}
