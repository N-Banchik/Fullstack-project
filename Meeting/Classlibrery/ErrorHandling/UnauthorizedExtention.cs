namespace DataAccess.ErrorHandling
{
    public class UnauthorizedExtention:Exception
    {
        public UnauthorizedExtention(string message)
        : base(message)
        {
        }
        public UnauthorizedExtention(string message, Exception inner)
       : base(message, inner)
        {
        }
    }
}
