namespace DataAccess.DTOs
{
    public class EventMemberDto
    {
        public int Id  { get; set; }
        public string? UserName { get; set; }
        public string? PhotoUrl { get; set; }
        public bool Arriving { get; set; }
    }
}
