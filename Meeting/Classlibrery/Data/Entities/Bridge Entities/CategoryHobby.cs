using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities.Bridge_Entities
{
    public class CategoryHobby
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int HobbyId { get; set; }
        public Category? Category { get; set; }
        public Hobby? Hobby { get; set; }
    }
}
