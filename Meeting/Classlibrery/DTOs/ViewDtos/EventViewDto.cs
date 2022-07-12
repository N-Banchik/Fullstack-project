namespace DataAccess.DTOs.ViewDtos
{
    public class EventViewDto
    {
        public int Id { get; set; }
        public string? EventTitle { get; set; }
        public string? EventDescription { get; set; }
        public string? EventLocation { get; set; }
        public DateTime EventDate { get; set; }
        public string? MainPhotoUrl { get; set; }
        
    }
}
