using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int CreatorId { get; set; }
        public User? Creator { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public DateTime? EditTime { get; set; }
        public bool Deleted { get; set; }
    }
}
