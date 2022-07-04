using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities.Bridge_Entities
{
    public class UserHobby
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HobbyId { get; set; }
        public User? User { get; set; }
        public Hobby? Hobby { get; set; }
        public bool Following { get; set; }
    }
}
