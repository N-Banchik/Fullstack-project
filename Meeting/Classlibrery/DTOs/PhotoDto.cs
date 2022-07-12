namespace DataAccess.DTOs
{
    public class PhotoDto
    {
        public int id { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
        public string? UploaderUserName { get; set; }
        public DateTime DateUploded { get; set; }
    }
}
