using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Creation_Dtos
{
    public class HobbyCreationDto
    {
        public string? HobbyName { get; set; }
        public string? Description { get; set; }
        public string? Rules { get; set; }
        public string? KeyFeatures { get; set; }
        public int CategoryId { get; set; }
    }
}
