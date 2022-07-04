using DataAccess.Data.Entities.Bridge_Entities;
using DataAccess.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Hobby
    {
        public int Id { get; set; }
        public string? HobbyName { get; set; }
        public string? Description { get; set; }
        public string? Rules { get; set; }
        public string? KeyFeatures { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int photoId { get; set; }
        public Photo<Hobby>? Photo { get; set; }


        public ICollection<Event>? Events { get; set; }
        public ICollection<Guide>? Guides { get; set; }
        public ICollection<UserHobby> Users { get; set; } = new List<UserHobby>();
        
    }
}
