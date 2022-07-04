using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.ViewDtos
{
    public class HobbyViewDto
    {
        public int Id { get; set; }
        public string? HobbyName { get; set; }
        public string? Description { get; set; }
        public string? KeyFeatures { get; set; }
        public string? MainPhotoUrl { get; set; }
    }
}
