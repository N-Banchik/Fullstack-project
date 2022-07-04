using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.UpdateDtos
{
    public class UpdateHobbyCategoryDto
    {
        public int HobbyId { get; set; }
        public int CategoryId { get; set; }
    }
}
