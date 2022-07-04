using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }

        public PhotoDto? Photo { get; set; }
        public ICollection<HobbyDto>? Hobbies { get; set; }
        public ICollection<EventDto>? EventsAttend { get; set; }
        public ICollection<GuideDto>? Guides { get; set; }
        public ICollection<EventDto>? EventsCreated { get; set; }
        public ICollection<PostDto>? Posts { get; set; }
    }
}
