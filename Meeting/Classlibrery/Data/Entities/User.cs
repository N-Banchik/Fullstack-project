using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Classlibrery.Data.Entities
{
    public class User:IdentityUser<int>
    {
        
        public string FullName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
        public DateTime DateOfBirth { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; } 

        //hobbies
        //events

    }
}
