namespace DataAccess.DTOs.Creation_Dtos
{
    public class EventCreationDto
    {
        public string? EventTitle { get; set; }
        public string? EventDescription { get; set; }
        public string? EventRules { get; set; }
        public DateTime EventDate { get; set; }
        public string? EventLocation { get; set; }
        public int HobbyID { get; set; }

    }
}
