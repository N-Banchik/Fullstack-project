using DataAccess.Data.Entities.Bridge_Entities;
using DataAccess.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Hobby:IPhotoble<Hobby>
    {
        public int Id { get; set; }
        public string? HobbyName { get; set; }
        public string? Description { get; set; }
        public string? Rules { get; set; }
        public string? KeyFeatures { get; set; }

        public ICollection<Photo<Hobby>>? Photos{ get; set; }
        public ICollection<Event>? Events { get; set; }
        public ICollection<Guide>? Guides { get; set; }
        public ICollection<CategoryHobby>? Categories { get; set; }
        public ICollection<UserHobby>? Users { get; set; }
        
    }
}
