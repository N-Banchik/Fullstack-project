using DataAccess.Data.Entities.Bridge_Entities;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data.Entities
{
    public class User : IdentityUser<int>
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
        public DateTime DateOfBirth { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }

        public int PhotoId { get; set; }
        public Photo<User>? Photo { get; set; }
        public ICollection<UserHobby>? Hobbies { get; set; }
        public ICollection<UserEvent>? EventsAttend { get; set; }
        public ICollection<Guide>? Guides { get; set; }
        public ICollection<Event>? EventsCreated { get; set; }
        public ICollection<Post>? Posts { get; set; }


    }
}
