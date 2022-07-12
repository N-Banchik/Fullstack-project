using DataAccess.DTOs.ViewDtos;

namespace DataAccess.DTOs
{
    public class HobbyDto
    {
        public int Id { get; set; }
        public string? HobbyName { get; set; }
        public string? Description { get; set; }
        public string? Rules { get; set; }
        public string? KeyFeatures { get; set; }
        public int CategoryId { get; set; }
        public PhotoDto? Photo { get; set; }
        public ICollection<GuideViewDto>? Guides{ get; set; }
        public ICollection<EventViewDto>? events{ get; set; }
        
        


    }
}
