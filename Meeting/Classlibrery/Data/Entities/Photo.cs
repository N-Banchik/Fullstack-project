namespace DataAccess.Data.Entities
{
    public class Photo<T> where T : class
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Descrption { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }
        public DateTime DateUploded { get; set; } = DateTime.Now;
        public T? Entity { get; set; }
        public int EntityId { get; set; }
        public int UploaderID { get; set; }
        public User? Uploader { get; set; }
    }
}
