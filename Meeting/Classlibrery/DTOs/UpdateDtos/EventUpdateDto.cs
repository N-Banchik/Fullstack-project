namespace DataAccess.DTOs.UpdateDtos
{
    public class EventUpdateDto
    {
        public int Id { get; set; }
        public string? EventTitle { get; set; }
        public string? EventDescription { get; set; }
        public string? EventRules { get; set; }
        public DateTime EventDate { get; set; }
        public string? EventLocation { get; set; }
        public bool Canceled { get; set; }
    }
}
