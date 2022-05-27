namespace Movie.Theater.Enterprises.Utilities.ExceptionHandler
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
