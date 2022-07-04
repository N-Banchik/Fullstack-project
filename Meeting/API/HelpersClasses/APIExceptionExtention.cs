namespace API.HelpersClasses
{
    public class APIExceptionExtention
    {
        public int StatusCode { get; }
        public string? Message { get; }
        public string? Details { get; }

        public APIExceptionExtention(int statusCode, string? message = null, string? details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
